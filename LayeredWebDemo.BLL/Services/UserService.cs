using System.Collections.Generic;
using AutoMapper;
using LayeredWebDemo.BLL.DTO;
using LayeredWebDemo.BLL.Interfaces;
using LayeredWebDemo.DAL.Interfaces;
using LayeredWebDemo.DAL.Entities;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using LayeredWebDemo.BLL.Config;

namespace LayeredWebDemo.BLL.Services
{
    public class UserService : IUserService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationSignInManager SignInManager { get; private set; }

        public ApplicationUserManager UserManager { get; private set; }
        #endregion

        public UserService(IUnitOfWork unitOfWork, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _unitOfWork = unitOfWork;
            UserManager = userManager;
            SignInManager = signInManager; 
        }
        /// <summary>
        /// Comment Description: 
        /// </summary>
        /// <returns>{Type of Request that has used this method}/{Controller}/{Action Name}</returns>   
        //
        // GET: /User/GetAll
        public IEnumerable<UserDTO> GetUsers() => Mapper.Map<IEnumerable<ApplicationUser>, List<UserDTO>>(_unitOfWork.Users.GetAll());

        //
        // GET: /User/Edit
        public  UserDTO GetUserById(string UserId) => Mapper.Map<ApplicationUser, UserDTO>(UserManager.FindById(UserId));

        //
        // POST: /User/Edit
        public async Task<IdentityResult> UpdateUserAsync(UserDTO model)
        {
            ApplicationUser user = UserManager.FindById(model.Id);
            return await UserManager.UpdateAsync(Mapper.Map(model, user));              
        }

        //
        // POST: /Account/Login
        public async Task<SignInStatus> PasswordSignInAsync(string Email, string Password, bool RememberMe = false, bool shouldLockout = false)
        {      
            return  await SignInManager.PasswordSignInAsync(Email, Password, RememberMe, shouldLockout: shouldLockout);                        
        }

        //
        // POST: /Account/ExternalLoginConfirmation & Register
        public async Task SignInAsync(UserDTO user, bool isPersistent = false, bool rememberBrowser = false)
        {
            await SignInManager.SignInAsync(Mapper.DynamicMap<ApplicationUser>(user), isPersistent, rememberBrowser);
        }

        //
        // GET: /Account/ExternalLoginCallback
        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo user, bool isPersistent = false) => await SignInManager.ExternalSignInAsync(user, isPersistent);

        //
        // POST: /Account/ExternalLoginConfirmation
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login) => await UserManager.AddLoginAsync(userId, login);

        //
        // POST: /Account/Register
        public async Task<IdentityResult> CreateAsync<T>(T model, string password = null)
        {
            ApplicationUser user = Mapper.DynamicMap<ApplicationUser>(model);
            return await UserManager.CreateAsync(user, password);
        }

        //
        // GET: /Account/ConfirmEmail
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token) => await UserManager.ConfirmEmailAsync(userId, token);

        //
        // POST: /Account/ForgotPassword
        public async Task<bool> IsEmailConfirmedAsync(string userId) => await UserManager.IsEmailConfirmedAsync(userId);

        //
        // POST: /Account/ForgotPassword
        public async Task<string> GeneratePasswordResetTokenAsync(string userId) => await UserManager.GeneratePasswordResetTokenAsync(userId);

        //
        // POST: /Account/ForgotPassword
        public async Task SendEmailAsync(string userId, string subject, string body) => await UserManager.SendEmailAsync(userId, subject, body);

        // POST: /Account/ResetPassword
        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword) => await UserManager.ResetPasswordAsync(userId, token, newPassword);

        // Dispose Database
        public void Dispose()
        {
            _unitOfWork.Dispose();
            UserManager?.Dispose();
            SignInManager?.Dispose();
        }
    }
}