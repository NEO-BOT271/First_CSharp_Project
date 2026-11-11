using System.Text.Json;

namespace Project_IT_STEP.Services;

internal class JsonStorage<T>
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    private readonly string FilePath;

    public JsonStorage(string fileName)
    {
        var folder = System.IO.Path.Combine(AppContext.BaseDirectory, "Data");
        Directory.CreateDirectory(folder);
        FilePath = System.IO.Path.Combine(folder, fileName);
    }

    public List<T> Load()
    {
        if (!File.Exists(FilePath))
            return [];

        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<List<T>>(stream) ?? [];
    }

    public void Save(List<T> items)
    {
        using var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
        JsonSerializer.Serialize(stream, items, Options);
    }
}