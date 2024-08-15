using System.Text.Json;

namespace InvesTechPlanner.Web.Services;

public class OptionsService(IHostEnvironment environment)
{
    private readonly IHostEnvironment _environment = environment;

    public async Task<Dictionary<string, List<string>>> GetOptionsAsync(string fileName)
    {
        // 修正: ファイル名を引数として受け取る
        var path = Path.Combine(_environment.ContentRootPath, "Components", "Data", fileName);

        if (File.Exists(path))
        {
            var json = await File.ReadAllTextAsync(path);
            var options = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
            return options ?? [];
        }
        else
        {
            Console.WriteLine("File not found.");
        }

        return [];
    }
}
