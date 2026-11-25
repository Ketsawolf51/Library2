using Microsoft.EntityFrameworkCore;

namespace Library2.Data;

public class Dbcontext:DbContext
{
    public DbSet<Book> Books { get; set; }
    public  DbSet<User> Users { get; set; }
    public DbSet<Author>  Authors { get; set; }
    public DbSet<Feedback>  Feedbacks { get; set; }
    public DbSet<Issuance>  Issuances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(  "Host=localhost;Port=5432;Database=GamificationDb;Username=postgres;Password=123456789");
    }

}