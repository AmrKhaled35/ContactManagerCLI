using ContactManagerCLI.Domain;
using ContactManagerCLI.Storage;

namespace ContactManagerCLI.Services;

public class ContactService : IContactService
{
    private readonly IStorageService _storage;
    private readonly List<Contact> _contacts;

    public ContactService(IStorageService storage)
    {
        _storage = storage;
        _contacts = _storage.Load();
    }

    public void Add(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void Edit(Guid id, string name, string phone, string email)
    {
        var contact = GetById(id);
        if (contact == null) return;

        contact.Name = name;
        contact.Phone = phone;
        contact.Email = email;
    }

    public void Delete(Guid id)
    {
        _contacts.RemoveAll(c => c.Id == id);
    }

    public Contact? GetById(Guid id)
    {
        return _contacts.FirstOrDefault(c => c.Id == id);
    }

    public List<Contact> GetAll()
    {
        return _contacts;
    }

    public List<Contact> Search(string keyword)
    {
        return _contacts.Where(c =>
                c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(keyword))
            .ToList();
    }

    public List<Contact> FilterByDate(DateTime date)
    {
        return _contacts
            .Where(c => c.CreationDate.Date == date.Date)
            .ToList();
    }

    public void Save()
    {
        _storage.Save(_contacts);
    }
}