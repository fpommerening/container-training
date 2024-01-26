using FP.ContainerTraining.Initializer.Init;
using MySqlConnector;

static async Task CheckOrAddTable(MySqlConnection connection)
{
    var cmdCheck = new MySqlCommand($"SELECT 1 FROM information_schema.tables WHERE table_schema = '{connection.Database}' AND table_name = 'versions' LIMIT 1;", connection);
    var resulCheck = await cmdCheck.ExecuteScalarAsync();
    if (resulCheck == null)
    {
        var cmdCreate = new MySqlCommand("CREATE TABLE versions (Id int not null primary key, CreatedAt datetime not null)", connection);
        await cmdCreate.ExecuteNonQueryAsync();
    }
}

static async Task CheckOrAddVersion(MySqlConnection connection, int targetId)
{
    var cmdCheck = new MySqlCommand("SELECT Id, CreatedAt FROM versions ORDER BY id DESC LIMIT 1;", connection);

    int? lastId = null;

    using (var reader = await cmdCheck.ExecuteReaderAsync())
    {
        if (reader.Read())
        {
            var id = reader.GetInt32("Id");
            var createAt = reader.GetDateTime("CreatedAt");
            Console.WriteLine($"Current Version ID: {id}  Created AT {createAt}");
            lastId = id;
        }
    }

    if (!lastId.HasValue || lastId.Value < targetId)
    {
        Console.WriteLine("Version missmatch detected");
        Console.WriteLine($"Updating from {lastId} to {targetId}");

        await Task.Delay(TimeSpan.FromSeconds(15));

        var cmdInsert = new MySqlCommand("INSERT INTO versions (Id, CreatedAt) VALUES (@id, @createdAt);", connection);
        cmdInsert.Parameters.AddWithValue("@id", targetId);
        cmdInsert.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
        await cmdInsert.ExecuteNonQueryAsync();

        await Task.Delay(TimeSpan.FromSeconds(15));

        Console.WriteLine($"Updated from {lastId} to {targetId}");
    }
}

try
{
    var cnn = EnvironmentVariable.GetValueOrDefault("MYSQL_CNN",
        "Server=127.0.0.1;Database=demo;Uid=root;Pwd=my-secret-pw;");
    var target = 1;
    if (args.Length == 1 && Int32.TryParse(args[0], out var t))
    {
        target = t;
    }

    await using var connection = new MySqlConnection(cnn);
    await connection.OpenAsync();
    await CheckOrAddTable(connection);
    await CheckOrAddVersion(connection, target);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
