using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
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
                optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=myDataBase;Uid=root;Pwd=Spasepederast1;", ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=myDataBase;Uid=root;Pwd=Spasepederast1;"), null);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTeacher>().HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId);
            modelBuilder.Entity<DocumentTeacher>().HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId);
            modelBuilder.Entity<User>().HasMany(m => m.DocumentsTeacher).WithOne(m => m.Sender).HasForeignKey(m => m.SenderId).IsRequired(true);
            modelBuilder.Entity<DocumentHeadMaster>().HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId);
            modelBuilder.Entity<DocumentHeadMaster>().HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId);
            modelBuilder.Entity<User>().HasMany(m => m.DocumentsHeadMaster).WithOne(m => m.Sender).HasForeignKey(m => m.SenderId).IsRequired(true);
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DocumentHeadMaster> DocumentsHeadMaster { get; set; }

        public DbSet<DocumentTeacher> DocumentsTeachers { get; set; }

    }
}
