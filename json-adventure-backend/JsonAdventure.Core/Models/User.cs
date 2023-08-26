using JsonAdventure.Core.Constants;

namespace JsonAdventure.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public User()
        {
            Name = string.Empty;
            Password = string.Empty;
            Role = Roles.None;
        }
    }
}