using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class IdentityContext
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        ExamDbContext context;

        public IdentityContext(ExamDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Seeding Data with this Project

        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            //await context.Database.MigrateAsync();

            int roles = await context.Roles.CountAsync();

            if (roles == 0)
            {
                await ConfigureRolesAsync();
            }

            int userRoles = await context.UserRoles.CountAsync();

            if (userRoles == 0)
            {
                await ConfigureAdminAccountAsync(adminPass, adminEmail);
            }
        }

        private async Task ConfigureRolesAsync()
        {
            // NormalizedName is not set by Identity :[ but when you want to find role by its name
            // remember that you are checking Normalized column's value! That is why I am adding NormalizedName value!
            IdentityRole admin = new IdentityRole(Role.Administrator.ToString()) { NormalizedName = Role.Administrator.ToString().ToUpper() };
            IdentityRole student = new IdentityRole(Role.Student.ToString()) { NormalizedName = Role.Student.ToString().ToUpper() };
            IdentityRole teacher = new IdentityRole(Role.Teacher.ToString()) { NormalizedName = Role.Teacher.ToString().ToUpper() };
            IdentityRole headmaster = new IdentityRole(Role.HeadMaster.ToString()) { NormalizedName = Role.HeadMaster.ToString().ToUpper() };

            context.Roles.Add(admin);
            context.Roles.Add(student);
            context.Roles.Add(teacher);
            context.Roles.Add(headmaster);
            await context.SaveChangesAsync();
        }

        private async Task ConfigureAdminAccountAsync(string password, string email)
        {
            User adminIdentityUser = await context.Users.FirstOrDefaultAsync();

            if (adminIdentityUser == null)
            {
                adminIdentityUser = new User("admin", "Admin Adminov");
                context.Users.Add(adminIdentityUser);
                await context.SaveChangesAsync();
            }

            await userManager.AddToRoleAsync(adminIdentityUser, Role.Administrator.ToString());
            await userManager.AddPasswordAsync(adminIdentityUser, password);
            await userManager.SetEmailAsync(adminIdentityUser, email);

        }

        #endregion

        #region CRUD

        public async Task CreateUserAsync(string username, string password, string email, string name, int age, Role role)
        {
            try
            {
                User user = new User(username, email, age, name);
                IdentityResult result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new ArgumentException(result.Errors.First().Description);
                }

                if (role == Role.Administrator)
                {
                    await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                }
                else if (role == Role.Student)
                {
                    await userManager.AddToRoleAsync(user, Role.Student.ToString());
                }
                else if (role == Role.Teacher)
                {
                    await userManager.AddToRoleAsync(user, Role.Teacher.ToString());
                }
                else if (role == Role.HeadMaster)
                {
                    await userManager.AddToRoleAsync(user, Role.HeadMaster.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogInUserAsync(string username, string password)
        {
            try
            {
                User user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);

                if (result.Succeeded)
                {
                    User loggedUser = await context.Users
                        .Include(u => u.Documents)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);

                    return await signInManager.CreateUserPrincipalAsync(loggedUser);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity is not null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return new ClaimsPrincipal();
            }

            // If should always be true when you call this method!
            return claimsPrincipal;
        }

        public async Task<User> ReadUserAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await userManager.Users
                        .Include(u => u.Documents)
                        .FirstOrDefaultAsync(u => u.Id == key);
                }

                return await userManager.FindByIdAsync(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAllUsersAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<User> query = userManager.Users;

                if (useNavigationalProperties)
                {
                    query = query.Include(u => u.Documents);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserAsync(User item, bool useNavigationalProperties = false)
        {
            try
            {
                User user = await ReadUserAsync(item.Id, useNavigationalProperties);

                if (user != null)
                {
                    user.UserName = item.UserName;
                    user.Age = item.Age;
                    user.Name = item.Name;

                    if (useNavigationalProperties)
                    {
                        List<Document> documents = new List<Document>();

                        foreach (Document doc in item.Documents)
                        {
                            Document docFromDb = await context.Documents.FindAsync(doc.EntryNumber);

                            if (docFromDb != null)
                            {
                                documents.Add(docFromDb);
                            }
                            else
                            {
                                documents.Add(doc);
                            }
                        }
                        user.Documents = documents;
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            try
            {
                User user = await ReadUserAsync(id);

                if (user == null)
                {
                    throw new ArgumentException("There is no user in the database with that id!");
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserByUsernameAsync(string username)
        {
            try
            {
                User user = await FindUserByNameAsync(username);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Find Methods

        public async Task<User> FindUserByNameAsync(string name, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await context.Users
                        .Include(u => u.Documents)
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == name.ToUpper());
                }

                // Identity return Null if there is no user!
                return await userManager.FindByNameAsync(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string key)
        {
            User user = context.Users.Find(key);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
