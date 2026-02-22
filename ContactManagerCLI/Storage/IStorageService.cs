using ContactManagerCLI.Domain;

namespace ContactManagerCLI.Storage;

public interface IStorageService
{
    List<Contact> Load();
    void Save(List<Contact> contacts);
}