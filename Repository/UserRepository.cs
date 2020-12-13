using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Repository.Models;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<EFUser> userManager;
        private readonly RoleManager<EFUserRole> roleManager;
        private readonly SignInManager<EFUser> signInManager;
        private readonly IMapper mapper;

        public UserRepository(
            UserManager<EFUser> userManager, 
            RoleManager<EFUserRole> roleManager, 
            SignInManager<EFUser> signInManager,
            IMapper mapper
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
           return (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IdentityResult> GetCreateResultAsync(string email, string username, string password)
        {
            var efUser = new EFUser { Email = email, UserName = username};

            return await userManager.CreateAsync(efUser, password);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            var efUser = await userManager.FindByNameAsync(user.UserName);
            
            return await userManager.GenerateEmailConfirmationTokenAsync(efUser);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            var efUser = await userManager.FindByIdAsync(id);
            
            return mapper.Map<User>(efUser);
        }

        public async Task<IdentityResult> GetConfirmEmailResultAsync(User user, string code)
        {
            var efUser = await userManager.FindByNameAsync(user.UserName);
            
            return await userManager.ConfirmEmailAsync(efUser, code);
        }
        
        public async Task<User> FindByNameAsync(string name)
        {
            var efUser = await userManager.FindByNameAsync(name);
            
            return mapper.Map<User>(efUser);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user)
        {
            var efUser = await userManager.FindByNameAsync(user.UserName);

            return await userManager.IsEmailConfirmedAsync(efUser);
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password)
        {
            return await signInManager.PasswordSignInAsync(username, password, false, false);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            return await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                false, true);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task ExternalAuthenticationLogin(string email, ExternalLoginInfo info)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new EFUser
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user);
            }
                    
            await userManager.AddLoginAsync(user, info);
            await signInManager.SignInAsync(user, false);
        }

        public List<User> GetAllUsers()
        {
            var efList = userManager.Users.ToList();
            var list = new List<User>();
            
            foreach (var efUser in efList)
            {
                 list.Add(mapper.Map<User>(efUser));
            }

            return list;
        }

        public async Task DeleteAsync(User user)
        {
            var efUser = await userManager.FindByNameAsync(user.UserName);
            
            await userManager.DeleteAsync(efUser);
        }
    }
}