using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HobbyContext : IDb<Hobby, int>, IQueryDb<Hobby, int>
    {
        BlazorDbContext context;

        public HobbyContext(BlazorDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Hobby item)
        {
            try
            {
                context.Hobbies.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception) // Write specific Exceptions and operations here!
            {
                throw;
            }
        }

        public async Task<Hobby> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Hobby> query = context.Hobbies;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.Users);
                }

                return await query.FirstOrDefaultAsync(h => h.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Hobby>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Hobby> query = context.Hobbies;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.Users);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The hobby won't update the collection of users! 
        /// </summary>
        /// <param name="item">The hobby which name was changed!</param>
        /// <param name="useNavigationalProperties">The User will update his hobbies. Does not make sense to pass true!</param>
        /// <returns></returns>
        public async Task UpdateAsync(Hobby item, bool useNavigationalProperties = false)
        {
            try
            {
                Hobby hobbyFromDb = await ReadAsync(item.Id);
                hobbyFromDb.Name = item.Name;

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Hobby hobby = await context.Hobbies.FindAsync(key);

                if (hobby == null)
                {
                    throw new ArgumentException("Hobby does not exist!");
                }

                context.Hobbies.Remove(hobby);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(int key)
        {
            Hobby hobby = context.Hobbies.Find(key);
            if (hobby == null)
            {
                return false;
            }
            return true;

            // Any can throw exception!
            //return context.Hobbies.Any(h => h.Id == key);
        }

        /// <summary>
        /// Returns the hobbies belonging to a user
        /// </summary>
        /// <param name="userId">Supply the id of the user</param>
        /// <returns></returns>
        public async Task<IEnumerable<Hobby>> FindHobbies(string userId)
        {
            User user = await context.Users.Include(u => u.Hobbies)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user.Hobbies;
        }

        // Wrapper used in HobbyToUser() in HobbiesController!
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

    }
}