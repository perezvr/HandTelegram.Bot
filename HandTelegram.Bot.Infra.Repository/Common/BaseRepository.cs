using HandTelegram.Bot.Domain.Interfaces.Repository.Common;
using HandTelegram.Bot.Domain.Models.Common;
using HandTelegram.Bot.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HandTelegram.Bot.Infra.Repository.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MasterDataDbContext _context;
        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(MasterDataDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public T Add(T obj)
        {
            DbSet.Add(obj);
            _context.SaveChanges();

            return obj;
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            DbSet.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<T> Get()
            => DbSet.ToList();

        public T Get(int id)
            => DbSet.Find(id);

        public T Get(long id)
           => DbSet.Find(id);
    }
}
