using Microsoft.EntityFrameworkCore;

namespace TestPostgresql;

public class BlogContext : DbContext
{
    const string connString = "Host=localhost;Username=philippe;Password=Nddclcp42;Database=postgres";

    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use NodaTime
        optionsBuilder.UseNpgsql(connString, 
            options => options.UseNodaTime());
    }
}

// /opt/homebrew/opt/postgresql/bin/postgres -D /opt/homebrew/var/postgres