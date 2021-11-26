using NodaTime;
using Npgsql;

namespace TestPostgresql;
    
// https://www.nuget.org/packages/Npgsql
// Install-Package Npgsql -Version 6.0.0
// https://www.nuget.org/packages/Npgsql.NodaTime
// Install-Package Npgsql.NodaTime -Version 6.0.0

public class PostgresqlTester
{
    const string connString = "Host=localhost;Username=philippe;Password=Nddclcp42;Database=postgres";

    public async Task Test1()
    {
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        // Seems that by default names are lowercase, so we need to use "Blogs" or "Name" to keep uppercase
        // Insert some data
        await using (var cmd = new NpgsqlCommand("INSERT INTO \"Blogs\" (\"Name\") VALUES (@p)", conn))
        {
            cmd.Parameters.AddWithValue("p", "Hello world");
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task Test2()
    {
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
        
        // Retrieve all rows
        await using (var cmd = new NpgsqlCommand("SELECT \"Name\" FROM \"Blogs\"", conn))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
                Console.WriteLine(reader.GetString(0));
        }
    }

    public async Task Test3()
    {
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        // Write NodaTime Instant to PostgreSQL "timestamp with time zone" (UTC)
        await using (var cmd = new NpgsqlCommand("INSERT INTO \"Movie\" (\"Title\", \"ReleaseDate\", \"Genre\", \"Price\") VALUES (@p1, @p2, @p3, @p4)", conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter("p1", "Title"));
            cmd.Parameters.Add(new NpgsqlParameter("p2", Instant.FromUtc(2011, 1, 1, 10, 30)));
            cmd.Parameters.Add(new NpgsqlParameter("p3", "Western"));
            cmd.Parameters.Add(new NpgsqlParameter("p4", 9.99));
            cmd.ExecuteNonQuery();
        }

// Read timestamp back from the database as an Instant
// SELECT "ReleaseDate" FROM "Movie"
        await using (var cmd = new NpgsqlCommand("SELECT \"ReleaseDate\" FROM \"Movie\"", conn))
        await using (var reader = cmd.ExecuteReader())
        {
            reader.Read();
            var instant = reader.GetFieldValue<Instant>(0);
        }
    }
}