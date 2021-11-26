using Npgsql;

namespace TestPostgresql;
    
// https://www.nuget.org/packages/Npgsql
// Install-Package Npgsql -Version 6.0.0

public class PostgresqlTester
{
    const string connString = "Host=localhost;Username=philippe;Password=Nddclcp42;Database=postgres";

    public async Task Test1()
    {
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

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
}