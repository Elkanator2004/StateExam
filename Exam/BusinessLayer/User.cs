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

        // Prevent loops if you are using Net Core Json Serializer!
        //[JsonIgnore]
        public ICollection<DocumentTeacher> DocumentsTeacher { get; set; }
        public ICollection<DocumentHeadMaster> DocumentsHeadMaster { get; set; }

        public User()
        {
            this.DocumentsTeacher = new List<DocumentTeacher>();
            this.DocumentsHeadMaster = new List<DocumentHeadMaster>();
        }

        public User(string username, string name) : base(username)
        {
            Name = name;
            this.DocumentsTeacher = new List<DocumentTeacher>();
            this.DocumentsHeadMaster = new List<DocumentHeadMaster>();
        }

        public User(string username, string email, int age, string name)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            this.Email = email;
            this.NormalizedEmail = email.ToUpper();
            this.Age = age;
            this.Name = name;
            this.DocumentsTeacher = new List<DocumentTeacher>();
            this.DocumentsHeadMaster = new List<DocumentHeadMaster>();
        }

        public User(string id, string username, string email, int age, string name)
            : this(username, email, age, name)
        {
            this.Id = id;
        }

        public User(string id, string username, string email, int age, string name, ICollection<DocumentTeacher> documentsteach, ICollection<DocumentHeadMaster> documentsheadmaster)
            : this(id, username, email, age, name)
        {
            DocumentsTeacher = documentsteach;
            DocumentsHeadMaster = documentsheadmaster;
        }

        public static explicit operator User(ValueTask<IdentityUser> v)
        {
            throw new NotImplementedException();
        }
    }
}