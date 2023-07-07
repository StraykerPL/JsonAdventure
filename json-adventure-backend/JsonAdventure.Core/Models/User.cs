namespace JsonAdventure.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User()
        {
            Name = string.Empty;
        }
    }
}