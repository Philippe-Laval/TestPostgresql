// See https://aka.ms/new-console-template for more information



using Microsoft.EntityFrameworkCore;
using TestPostgresql;

Console.WriteLine("Hello, Postgresql!");

var test = new PostgresqlTester();
// await test.Test1();
await test.Test2();

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