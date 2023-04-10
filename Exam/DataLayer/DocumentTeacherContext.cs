using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DocumentTeacherContext : IDB<DocumentTeacher, string>, IQueryDb<DocumentTeacher, string>
    {
        ExamDbContext context;

        public DocumentTeacherContext(ExamDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(DocumentTeacher item)
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

                context.DocumentsTeachers.Add(item);
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
                DocumentTeacher docFromDb = await context.DocumentsTeachers.FindAsync(key);

                if (docFromDb == null)
                {
                    throw new ArgumentException("Document does not exist!");
                }

                context.DocumentsTeachers.Remove(docFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DocumentTeacher>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<DocumentTeacher> query = context.DocumentsTeachers;

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

        public async Task<DocumentTeacher> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<DocumentTeacher> query = context.DocumentsTeachers;

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

        public async Task UpdateAsync(DocumentTeacher item, bool useNavigationalProperties = false)
        {
            try
            {
                context.DocumentsTeachers.Update(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<DocumentTeacher>> GetDocumentsTeachByUserIdAsync(string id)
        {
            try
            {
                return await context.DocumentsTeachers
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
            DocumentTeacher doc = context.DocumentsTeachers.Find(key);

            if (doc == null)
            {
                return false;
            }

            return true;
        }
    }
}
