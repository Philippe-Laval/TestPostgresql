// Npgsql Entity Framework Core Provider
// https://www.npgsql.org/efcore/index.html
// Use of nodatime in EF Core
// https://www.npgsql.org/efcore/mapping/nodatime.html

using Microsoft.EntityFrameworkCore;
using Npgsql;
using TestPostgresql;

Console.WriteLine("Hello, Postgresql!");

// Once this is done, you can simply use NodaTime types
// when interacting with PostgreSQL, just as you would use e.g. DateTime
NpgsqlConnection.GlobalTypeMapper.UseNodaTime();


var test = new PostgresqlTester();
// await test.Test1();
// await test.Test2();
// await test.Test3();

await using var ctx = new BlogContext();
//await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

var fooBlog = await ctx.Blogs.FirstOrDefaultAsync(o => o.Name.Equals("FooBlog"));
if (fooBlog == null)
{
    // Insert a Blog
    ctx.Blogs.Add(new Blog() { Name = "FooBlog" });
    await ctx.SaveChangesAsync();
}

// Query all blogs who's name starts with F
var blogs = await ctx.Blogs.Where(b => b.Name.StartsWith("F")).ToListAsync();
foreach (var blog in blogs)
{
    Console.WriteLine($"{blog.Id} {blog.Name}");
}