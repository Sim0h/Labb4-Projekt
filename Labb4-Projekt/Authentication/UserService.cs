using Labb4_Projekt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Labb4_Projekt.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return token;
        }

        public async Task SeedRolesAndUsersAync()
        {
            // Kontrollera och skapa roller om de inte finns
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Skapa adminanvändare om den inte finns
            var adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@example.com"
            };

            if (await _userManager.FindByNameAsync(adminUser.UserName) == null)
            {
                var result = await _userManager.CreateAsync(adminUser, "AdminPassword123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Skapa kundanvändare för varje kund i databasen om de inte finns
            var customers = _dbContext.Customers.ToList();
            foreach (var customer in customers)
            {
                if (await _userManager.FindByEmailAsync(customer.CustomerEmail) == null)
                {
                    var identityUser = new IdentityUser
                    {
                        UserName = customer.CustomerEmail,
                        Email = customer.CustomerEmail
                    };

                    var result = await _userManager.CreateAsync(identityUser, "CustomerPassword123!");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(identityUser, "Customer");
                        customer.IdentityUserId = identityUser.Id;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
