using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XHR.Context;
using XHR.Models;

namespace XHR.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(string username, string password)
        {
           

            var userExists = await _context.Users.AnyAsync(u => u.Username == username);
            if (userExists)
                throw new ArgumentException("Username already taken");

          
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> LoginAsync(string username, string password)
        {
      
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

           
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

          
            return GenerateJwtToken(user);
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
