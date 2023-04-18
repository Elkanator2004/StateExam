using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DocumentContext : IDB<Document, string>, IQueryDb<Document, string>
    {
        ExamDbContext context;

        public DocumentContext(ExamDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Document item)
        {
            try
            {
                context.Documents.Add(item);
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
                context.Documents.Remove(await ReadAsync(key));
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Document>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Document> query = context.Documents;

                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.SenderId).Include(p => p.ReceiverId);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Document> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Document> query = context.Documents;

                if (useNavigationalProperties)
                {
                    query = query.Include(p => p.SenderId).Include(p => p.ReceiverId);
                }

                return await query.FirstOrDefaultAsync(p => p.EntryNumber == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Document item, bool useNavigationalProperties = false)
        {
            try
            {
                Document documentFromDb = await context.Documents.FindAsync(item.EntryNumber);

                documentFromDb.Title = item.Title;
                documentFromDb.DateSigned= item.DateSigned;
                documentFromDb.DateReceived = item.DateReceived;

                if (documentFromDb is ThreeDays)
                {
                    (documentFromDb as ThreeDays).Days = (item as ThreeDays).Days;
                    (documentFromDb as ThreeDays).DateFrom = (item as ThreeDays).DateFrom;
                    (documentFromDb as ThreeDays).DateTo = (item as ThreeDays).DateTo;
                }
                else
                {
                    (documentFromDb as SevenDays).Days = (item as SevenDays).Days;
                    (documentFromDb as SevenDays).DateFrom = (item as SevenDays).DateFrom;
                }

                if (useNavigationalProperties)
                {
                    User sender = new User();
                    User receiver = new User();

                    User senderFromDb = context.Users.Find(item.SenderId);
                    User receiverFromDb = context.Users.Find(item.ReceiverId);

                    if (senderFromDb != null && receiverFromDb != null)
                    {
                        sender.Id= senderFromDb.Id;
                        receiver.Id= receiverFromDb.Id;
                    }
                    else if (senderFromDb == null)
                    {
                        sender = senderFromDb;
                    }
                    else if(receiverFromDb == null)
                    {
                        receiver = receiverFromDb;
                    }

                    documentFromDb.Sender = sender;
                    documentFromDb.Receiver = receiver;

                }
                //dbContext.Update(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Exists(string key)
        {
            return await context.Documents.AnyAsync(e => e.EntryNumber == key);
        }
    }
}
