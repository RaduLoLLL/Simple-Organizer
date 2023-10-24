using Microsoft.EntityFrameworkCore;
using Simple_Organizer_Backend;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Define DbSet properties for your entities
    public DbSet<User> User { get; set; }
    public DbSet<Item> items { get; set; }

}
