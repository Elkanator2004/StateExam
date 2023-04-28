using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class IdentityManager
    {
        IdentityContext context;
        UserManager<User> userManager;

        public IdentityManager(IdentityContext _context, UserManager<User> _manager)
        {
            this.userManager = _manager;
            this.context = _context;
        }
        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            await context.SeedDataAsync(adminPass, adminEmail);
        }
        public async Task CreateUserAsync(string username, string password, string email, string name, int age, Role role)
        {
            await context.CreateUserAsync(username, password, email, name, age, role);
        }
        public async Task CreateAdminAsync(string username, string password, string email, string name, int age)
        {
            await context.CreateUserAsync(username, password, email, name, age, Role.Administrator);
        }

        public async Task<ClaimsPrincipal> LogInUserAsync(string username, string password)
        {
            return await context.LogInUserAsync(username, password);
        }

        public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await context.LogOutUserAsync(claimsPrincipal);
        }

        public async Task<User> ReadUserAsync(string key, bool useNavigationalProperties = false)
        {
            return await context.ReadUserAsync(key, useNavigationalProperties);
        }

        public async Task<IEnumerable<User>> ReadAllUsersAsync(bool useNavigationalProperties = false)
        {
            IEnumerable<User> users = await context.ReadAllUsersAsync(useNavigationalProperties);

            return users;
        }

        public async Task UpdateUserAsync(User user, bool useNavigationalProperties = false)
        {
            await context.UpdateUserAsync(user, useNavigationalProperties);
        }

        public async Task DeleteUserAsync(string id)
        {
            await context.DeleteUserAsync(id);
        }

        public async Task DeleteUserByUsernameAsync(string username)
        {
            await context.DeleteUserByUsernameAsync(username);
        }
    }
}
