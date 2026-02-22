using System.Text.Json;
using ContactManagerCLI.Domain;
namespace ContactManagerCLI.Storage;
public class JsonStorage : IStorageService
{
    private readonly string _filePath = "contacts.json";

    public List<Contact> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Contact>();

        var json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Contact>>(json)
               ?? new List<Contact>();
    }

    public void Save(List<Contact> contacts)
    {
        var json = JsonSerializer.Serialize(contacts,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_filePath, json);
    }
}