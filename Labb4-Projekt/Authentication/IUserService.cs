using Microsoft.AspNetCore.Identity;

namespace Labb4_Projekt.Authentication
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task SeedRolesAndUsersAync();
        string GenerateJwtToken(IdentityUser user, IList<string> roles);
    }
}

