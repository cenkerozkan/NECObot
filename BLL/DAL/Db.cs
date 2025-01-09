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

            //(replace with actual connection string if needed)
            optionsBuilder.UseNpgsql("server=localhost;port=5432;database=postgres;uid=postgres;pwd=NECO_2024;");

            return new Db(optionsBuilder.Options);
        }
    }
    public class Db : DbContext
    {
        // These are the corresponding tables in the database.
        public DbSet<ChatThread> ChatThreads { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AcceptedMessage> AcceptedMessages { get; set; }
        public DbSet<MessageCategory> MessageCategories { get; set; }
        
        // This is the constructor that will be called when the application is run.
        public Db(DbContextOptions<Db> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure many-to-many relationship for UserRoles
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            
            // Configure many-to-many relationship for MessageCategories
            modelBuilder.Entity<MessageCategory>()
                .HasKey(mc => new { mc.CategoryId, mc.AcceptedMessageId });
            
            // Configure cascade delete behavior
            modelBuilder.Entity<AcceptedMessage>()
                .HasOne(am => am.Message)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
