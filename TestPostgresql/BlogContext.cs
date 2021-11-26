using Microsoft.EntityFrameworkCore;

public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Username=philippe;Password=Nddclcp42;Database=postgres");
    }
}

// /opt/homebrew/opt/postgresql/bin/postgres -D /opt/homebrew/var/postgres