using HandTelegram.Bot.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HandTelegram.Bot.Infra.Data
{
    public class MasterDataDbContext : DbContext
    {
        public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(x => x.Id);

                user.ToTable("User", "masterdata");

                user.Property(x => x.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                user.Property(x => x.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                user.Property(x => x.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
