using System.Text.Json;

namespace Project_IT_STEP.Services;

public class JsonStorage<T>
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    private readonly string Path;

    public JsonStorage(string path)
    {
        Path = path;
    }

    public List<T> Load() =>
        File.Exists(Path)
            ? JsonSerializer.Deserialize<List<T>>(File.ReadAllText(Path)) ?? []
            : [];

    public void Save(List<T> items) =>
        File.WriteAllText(Path, JsonSerializer.Serialize(items, Options));
}