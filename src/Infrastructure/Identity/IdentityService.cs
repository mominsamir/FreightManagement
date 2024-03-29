﻿using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace FreightManagement.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
  //      private readonly UserManager<ApplicationUser> _userManager;
  //      private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        /*

                    UserManager<ApplicationUser> userManager,
                    IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
         */
        public IdentityService(
            IAuthorizationService authorizationService)
        {
//            _userManager = userManager;
//            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new System.NotImplementedException();
        }

        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result> DeleteUserAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new System.NotImplementedException();
        }

        /*        public async Task<string> GetUserNameAsync(string userId)
                {
                    var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

                    return user.UserName;
                }

                public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
                {
                    var user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = userName,
                    };

                    var result = await _userManager.CreateAsync(user, password);

                    return (result.ToApplicationResult(), user.Id);
                }

                public async Task<bool> IsInRoleAsync(string userId, string role)
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

                    return await _userManager.IsInRoleAsync(user, role);
                }

                public async Task<bool> AuthorizeAsync(string userId, string policyName)
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

                    var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                    var result = await _authorizationService.AuthorizeAsync(principal, policyName);

                    return result.Succeeded;
                }

                public async Task<Result> DeleteUserAsync(string userId)
                {
                    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

                    if (user != null)
                    {
                        return await DeleteUserAsync(user);
                    }

                    return Result.Success();
                }

                public async Task<Result> DeleteUserAsync(ApplicationUser user)
                {
                    var result = await _userManager.DeleteAsync(user);

                    return result.ToApplicationResult();
                }
        */
    }
}
