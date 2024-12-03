using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        // This is for corresponding the model with the table in the database.
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Db(DbContextOptions<Db> options) : base(options) { }
    }
}