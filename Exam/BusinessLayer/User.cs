using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Range(0,100)]
        public int Age { get; set; }
        [MaxLength(30)]
        public string Adress { get; set; }
        [MaxLength(10)]
        public string Telephone { get; set; }

        public ICollection<Document> Documents { get; set; }

        public User()
        {
            this.Documents = new List<Document>();
        }

        public User(string username, string name) : base(username)
        {
            Name = name;
            this.Documents = new List<Document>();
        }

        public User(string username, string email, int age, string name)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            this.Email = email;
            this.NormalizedEmail = email.ToUpper();
            this.Age = age;
            this.Name = name;
            this.Documents = new List<Document>();
        }

        public User(string id, string username, string email, int age, string name)
            : this(username, email, age, name)
        {
            this.Id = id;
        }

        public User(string id, string username, string email, int age, string name, ICollection<Document> documents)
            : this(id, username, email, age, name)
        {
            Documents = documents;
        }

        public static explicit operator User(ValueTask<IdentityUser> v)
        {
            throw new NotImplementedException();
        }
    }
}