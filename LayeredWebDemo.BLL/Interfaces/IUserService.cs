using System.Collections.Generic;
using LayeredWebDemo.BLL.DTO;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LayeredWebDemo.DAL.Entities;

namespace LayeredWebDemo.BLL.Interfaces
{
    public interface IUserService
    {
        Task<SignInStatus> PasswordSignInAsync(string Email, string Password, bool RememberMe = false, bool shouldLockout = false);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo user, bool isPersistent = false);
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
        Task SignInAsync(UserDTO user, bool isPersistent = false, bool rememberBrowser = false);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        Task<IdentityResult> CreateAsync<T>(T model, string password = null);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task SendEmailAsync(string userId, string subject, string body);
        Task<string> GeneratePasswordResetTokenAsync(string userId);           
        Task<IdentityResult> UpdateUserAsync(UserDTO model);
        Task<bool> IsEmailConfirmedAsync(string userId);

        UserDTO GetUserById(string UserId);
        IEnumerable<UserDTO> GetUsers();
        void Dispose();
    }
}