using HandTelegram.Bot.Domain.Models.Common;
using System.Collections.Generic;

namespace HandTelegram.Bot.Domain.Interfaces.Repository.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Add(T t);
        void Update(T t);
        void Delete(T obj);
        IEnumerable<T> Get();
        T Get(int id);
        T Get(long id);
    }
}
