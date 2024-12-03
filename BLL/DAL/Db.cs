using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace BLL.DAL
{
    // This is because I am using MacOS :D
    // If I used UseSqlServer, .NET would automatically handle this process for me.
    // But since I am using PostgreSQL because MSSQL server does not run on
    // MacOS, I had to manually create this class.
    public class DbContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();

            // Use Npgsql with the connection string from appsettings.json (replace with actual connection string if needed)
            optionsBuilder.UseNpgsql("server=localhost;port=5432;database=postgres;uid=postgres;pwd=NECO_2024;");

            return new Db(optionsBuilder.Options);
        }
    }
    public class Db : DbContext
    {
        // This is for corresponding the model with the table in the database.
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Db(DbContextOptions<Db> options) : base(options) { }
    }
}