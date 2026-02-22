using ContactManagerCLI.Services;
using ContactManagerCLI.Storage;
using ContactManagerCLI.CLI;
namespace ContactManagerCLI;
class Program
{
    static void Main(string[] args)
    {
        IStorageService storage = new JsonStorage();
        IContactService service = new ContactService(storage);
        var menu = new Menu(service);
        menu.Start();
    }
}