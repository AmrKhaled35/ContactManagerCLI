using ContactManagerCLI.Domain;
namespace ContactManagerCLI.Services;
public interface IContactService
{
    void Add(Contact contact);
    void Edit(Guid id, string name, string phone, string email);
    void Delete(Guid id);
    Contact? GetById(Guid id);
    List<Contact> GetAll();
    List<Contact> Search(string keyword);
    List<Contact> FilterByDate(DateTime date);
    void Save();
}