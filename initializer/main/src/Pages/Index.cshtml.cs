using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace FP.ContainerTraining.Initializer.Main.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public List<Version> Versions = new();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        var cnn = EnvironmentVariable.GetValueOrDefault("MYSQL_CNN", "Server=127.0.0.1;Database=demo;Uid=root;Pwd=my-secret-pw;");
        await using var connection = new MySqlConnection(cnn);
        await connection.OpenAsync();
        var cmdCheck = new MySqlCommand("SELECT Id, CreatedAt FROM versions ORDER BY id DESC", connection);
        await using var reader = await cmdCheck.ExecuteReaderAsync();
        while (reader.Read())
        {
            var id = reader.GetInt32("Id");
            var createAt = reader.GetDateTime("CreatedAt");
            Versions.Add(new Version
            {
                CreatedOn = createAt,
                Number = id
            });
        }
    }
}