﻿using Boostup.API.Entities;
using Boostup.API.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;

namespace Boostup.API.Repositories.Auth
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<User> userManager;
        private static readonly string DefaultRole = "User";
        public UserManagerRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /*public async Task<User?> RegisterUser(RegisterRequestDto request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.UserName,
                FullName = request.FullName
            };

            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                return null;
            }

            // Add Role to User
            identityResult = await userManager.AddToRoleAsync(user, DefaultRole);

            if (!identityResult.Succeeded)
            {
                return null;
            }

            return user;

        }*/

        public async Task<Boolean> CheckPassword(User user, string password)
        {
            bool passwordMatch = await userManager.CheckPasswordAsync(user, password);
            return passwordMatch;
        }

        public async Task<List<string>?> GetRoles(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            if (roles == null)
            {
                return null;
            }

            return roles.ToList();
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return user;
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }

    }
}
