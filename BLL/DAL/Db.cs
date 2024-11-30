using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Db(DbContextOptions<Db> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.ChatThread)
                .WithMany(ct => ct.Messages)
                .HasForeignKey(m => m.ChatThreadId);
        }
    }
}