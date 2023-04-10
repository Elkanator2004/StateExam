using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MessageContext : IDb<Message, string>, IQueryDb<Message, string>
    {
        BlazorDbContext context;

        public MessageContext(BlazorDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Message item)
        {
            try
            {
                item.Id = Guid.NewGuid().ToString();
                User senderFromDb = await context.Users.FindAsync(item.SenderId);
                User receiverFromDb = item.ReceiverId != null ? await context.Users.FindAsync(item.ReceiverId) : null;

                if (senderFromDb != null)
                {
                    item.Sender = senderFromDb;
                }

                if (receiverFromDb != null)
                {
                    item.Receiver = receiverFromDb;
                }

                context.Messages.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception) // Write specific Exceptions and operations here!
            {
                throw;
            }
        }

        public async Task<Message> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Message> query = context.Messages;

                if (useNavigationalProperties)
                {
                    query = query.Include(m => m.Sender).Include(m => m.Receiver);
                }

                return await query.FirstOrDefaultAsync(m => m.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Message>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Message> query = context.Messages;

                if (useNavigationalProperties)
                {
                    query = query.Include(m => m.Sender).Include(m => m.Receiver);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Message item, bool useNavigationalProperties = false)
        {
            try
            {
                context.Messages.Update(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                Message messageFromDb = await context.Messages.FindAsync(key);

                if (messageFromDb == null)
                {
                    throw new ArgumentException("Message does not exist!");
                }

                context.Messages.Remove(messageFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(string id)
        {
            try
            {
                return await context.Messages
                    .Include(m => m.Sender).Include(m => m.Receiver)
                    .Where(m => m.SenderId == id).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string key)
        {
            Message message = context.Messages.Find(key);

            if (message == null)
            {
                return false;
            }

            return true;
        }
    }
}
