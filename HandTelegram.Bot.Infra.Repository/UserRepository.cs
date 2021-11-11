using HandTelegram.Bot.Domain.Interfaces.Repository;
using HandTelegram.Bot.Domain.Models;
using HandTelegram.Bot.Infra.Data;
using HandTelegram.Bot.Infra.Repository.Common;

namespace HandTelegram.Bot.Infra.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MasterDataDbContext context)
            : base(context)
        { }
    }
}
