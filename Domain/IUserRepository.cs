using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public interface IUserRepository
    {
        Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<IdentityResult> GetCreateResultAsync(string email, string username, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<User> FindByIdAsync(string id);
        Task<IdentityResult> GetConfirmEmailResultAsync(User user, string code);
        Task<User> FindByNameAsync(string name);
        Task<bool> IsEmailConfirmedAsync(User user);
        Task<SignInResult> PasswordSignInAsync(string username, string password);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo info);
        Task SignOutAsync();
        Task ExternalAuthenticationLogin(string email, ExternalLoginInfo info);
        List<User> GetAllUsers();
        Task DeleteAsync(User user);
    }
}