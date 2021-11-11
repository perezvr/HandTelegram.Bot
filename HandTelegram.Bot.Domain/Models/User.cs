using HandTelegram.Bot.Domain.Models.Common;

namespace HandTelegram.Bot.Domain.Models
{
    public class User : BaseEntity
    {
        public long Id { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool Admin { get; private set; }

        public User(long id, string username, string firstName, string lastName)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Admin = false;
        }
    }
}
