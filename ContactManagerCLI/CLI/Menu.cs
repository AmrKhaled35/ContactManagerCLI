using ContactManagerCLI.Domain;
using ContactManagerCLI.Services;
namespace ContactManagerCLI.CLI;
public class Menu
{
    private readonly IContactService _service;
    public Menu(IContactService service)
    {
        _service = service;
    }

    public void Start()
    {
        var contacts = _service.GetAll();

        if (contacts.Any())
            DisplayContacts(contacts);

        while (true)
        {
            Console.WriteLine("\n=== Contact Manager ===");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Edit Contact");
            Console.WriteLine("3. Delete Contact");
            Console.WriteLine("4. View Contact");
            Console.WriteLine("5. List Contacts");
            Console.WriteLine("6. Search");
            Console.WriteLine("7. Filter");
            Console.WriteLine("8. Save");
            Console.WriteLine("9. Exit");

            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Add();
                    break;

                case "2":
                    Edit();
                    break;

                case "3":
                    Delete();
                    break;

                case "4":
                    View();
                    break;

                case "5":
                    DisplayContacts(_service.GetAll());
                    break;

                case "6":
                    Search();
                    break;

                case "7":
                    Filter();
                    break;

                case "8":
                    _service.Save();
                    Console.WriteLine("Saved!");
                    break;

                case "9":
                    _service.Save();
                    return;
            }
        }
    }

    private void Add()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();

        Console.Write("Phone: ");
        var phone = Console.ReadLine();

        Console.Write("Email: ");
        var email = Console.ReadLine();

        var contact = new Contact(name!, phone!, email!);
        _service.Add(contact);
    }

    private void Edit()
    {
        Console.Write("Enter Id: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid Id format.");
            return;
        }

        var contact = _service.GetById(id);

        if (contact == null)
        {
            Console.WriteLine("Contact not found.");
            return;
        }

        Console.Write("New Name: ");
        var name = Console.ReadLine();

        Console.Write("New Phone: ");
        var phone = Console.ReadLine();

        Console.Write("New Email: ");
        var email = Console.ReadLine();

        _service.Edit(id, name!, phone!, email!);
    }
    private void Delete()
    {
        Console.Write("Enter Id: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid Id format.");
            return;
        }
        var contact = _service.GetById(id);

        if (contact == null)
        {
            Console.WriteLine("Contact not found.");
            return;
        }
        _service.Delete(id);
        Console.WriteLine("Contact deleted successfully.");
    }

    private void View()
    {
        Console.Write("Enter Id: ");
        var input = Console.ReadLine();

        if (!Guid.TryParse(input, out var id))
        {
            Console.WriteLine("Invalid Id format.");
            return;
        }
        var contact = _service.GetById(id);
        if (contact == null)
        {
            Console.WriteLine("Contact not found.");
            return;
        }

        Console.WriteLine($"Id: {contact.Id}");
        Console.WriteLine($"Name: {contact.Name}");
        Console.WriteLine($"Phone: {contact.Phone}");
        Console.WriteLine($"Email: {contact.Email}");
        Console.WriteLine($"Creation Date: {contact.CreationDate}");
    }

    private void Search()
    {
        Console.Write("Keyword: ");
        var keyword = Console.ReadLine();

        var results = _service.Search(keyword!);
        DisplayContacts(results);
    }

    private void Filter()
    {
        Console.Write("Enter Date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var date))
            return;

        var results = _service.FilterByDate(date);
        DisplayContacts(results);
    }

    private void DisplayContacts(List<Contact> contacts)
    {
        if (!contacts.Any())
        {
            Console.WriteLine("No contacts found.");
            return;
        }

        foreach (var c in contacts)
        {
            Console.WriteLine(
                $"{c.Id} | {c.Name} | {c.Phone} | {c.Email} | {c.CreationDate}");
        }
    }
}