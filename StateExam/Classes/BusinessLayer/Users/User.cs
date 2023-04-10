namespace Classes
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {

        }

        public User(string uname, string password, string email)
        {
            this.Username = uname;
            this.Password = password;
            this.Email = email;
        }
    }
}