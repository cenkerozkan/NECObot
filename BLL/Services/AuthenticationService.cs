using System.Security.Cryptography;
using System.Text;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IUserAuthenticationService
    {
        Task<(bool success, string message, User? user)> LoginAsync(string username, string password);
        Task<(bool success, string message)> RegisterAsync(string username, string password);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly Db _context;

        public UserAuthenticationService(Db context)
        {
            _context = context;
        }

        public async Task<(bool success, string message, User? user)> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return (false, "Invalid username or password.", null);

            if (!VerifyPassword(password, user.Password))
                return (false, "Invalid username or password.", null);

            return (true, "Login successful", user);
        }

        public async Task<(bool success, string message)> RegisterAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return (false, "Username already exists.");

            var regularRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "RegularUser");
            if (regularRole == null)
                return (false, "Role configuration error.");

            var user = new User
            {
                Username = username,
                Password = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = regularRole.Id
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return (true, "Registration successful");
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
} 