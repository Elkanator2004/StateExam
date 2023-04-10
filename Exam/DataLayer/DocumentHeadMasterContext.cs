using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DocumentHeadMasterContext : IDB<DocumentHeadMaster, string>, IQueryDb<DocumentHeadMaster, string>
    {
        ExamDbContext context;

        public DocumentHeadMasterContext(ExamDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(DocumentHeadMaster item)
        {
            try
            {
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

                context.DocumentsHeadMaster.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception) // Write specific Exceptions and operations here!
            {
                throw;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                DocumentHeadMaster docFromDb = await context.DocumentsHeadMaster.FindAsync(key);

                if (docFromDb == null)
                {
                    throw new ArgumentException("Document does not exist!");
                }

                context.DocumentsHeadMaster.Remove(docFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DocumentHeadMaster>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<DocumentHeadMaster> query = context.DocumentsHeadMaster;

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

        public async Task<DocumentHeadMaster> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<DocumentHeadMaster> query = context.DocumentsHeadMaster;

                if (useNavigationalProperties)
                {
                    query = query.Include(m => m.Sender).Include(m => m.Receiver);
                }

                return await query.FirstOrDefaultAsync(m => m.EntryNumber == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(DocumentHeadMaster item, bool useNavigationalProperties = false)
        {
            try
            {
                context.DocumentsHeadMaster.Update(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<DocumentHeadMaster>> GetDocumentsHeadMasterByUserIdAsync(string id)
        {
            try
            {
                return await context.DocumentsHeadMaster
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
            DocumentHeadMaster doc = context.DocumentsHeadMaster.Find(key);

            if (doc == null)
            {
                return false;
            }

            return true;
        }
    }
}
