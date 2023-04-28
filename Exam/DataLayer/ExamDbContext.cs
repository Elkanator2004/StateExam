using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ExamDbContext : IdentityDbContext<User>
    {
        public ExamDbContext() : base()
        {

        }

        public ExamDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server = DESKTOP-L2UH44P\SQLEXPRESS; Database = ExamDb; Trusted_Connection = True;");
            }
            

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SevenDays>();
            modelBuilder.Entity<ThreeDays>();

            modelBuilder.Entity<Document>().HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Document>().HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(m => m.Documents).WithOne(m => m.Sender).HasForeignKey(m => m.SenderId).IsRequired(true);
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Document> Documents { get; set; }

    }
}
