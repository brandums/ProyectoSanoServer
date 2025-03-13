using BackEndProyectoSano.DBaseContext;
using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("login/{name}/{password}/{token}")]
        public async Task<IActionResult> Login(string name, string password, string token)
        {
            User user = _context.Users.First(u => u.Email == name && u.Password == password);
            if (user != null)
            {
                user.Token = token;
                _context.Entry(user).State = EntityState.Modified;
                await SaveChangesAndSerializeAsync();

                return Ok(new { Message = "Inicio de sesión exitoso", User = user });
            }
            else
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }
        }


        [HttpPost("GenerateUsername")]
        public async Task<ActionResult<string>> GenerateUsername()
        {
            string baseUsername = "Temp";

            string generatedUsername;
            do
            {
                generatedUsername = $"{baseUsername}{(new Random().Next(0, 1000))}";
            } while (await _context.Users.AnyAsync(u => u.Name == generatedUsername));

            return Ok(generatedUsername);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users.Any(u => (u.Email == user.Email && !string.IsNullOrEmpty(u.Email))))
            {
                return BadRequest("El usuario o el correo electrónico ya están en uso.");
            }

            string code;
            do
            {
                code = (new Random().Next(10000, 100000)).ToString();
            } while (_context.Users.Any(u => u.AccountNumber == code));

            user.AccountNumber = code;

            _context.Users.Add(user);
            await SaveChangesAndSerializeAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await SaveChangesAndSerializeAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await SaveChangesAndSerializeAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private async Task SaveChangesAndSerializeAsync()
        {
            await _context.SaveChangesAsync();
            await SerializeToXmlAsync();
        }

        private async Task SerializeToXmlAsync()
        {
            var users = await _context.Users.ToListAsync();

            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "xmluploads");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, "users.xml");

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}
