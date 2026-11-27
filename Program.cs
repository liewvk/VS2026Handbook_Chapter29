using System.Text.Json;
const string FileName = "inventory.json";
List<Product> items = File.Exists(FileName)
? JsonSerializer.Deserialize<List<Product>>(
File.ReadAllText(FileName)) ?? new()
: new();
while (true)
{
    Console.Write("1=Add 2=List 0=Exit: ");
    string? choice = Console.ReadLine();
    if (choice == "0") break;
    if (choice == "2")
    {
        foreach (var item in items)
            Console.WriteLine($"{item.Name}: {item.Price:F2}");
    }
    else if (choice == "1")
    {
        Console.Write("Name: ");
        string name = (Console.ReadLine() ?? "").Trim();
        Console.Write("Price: ");
        if (name.Length == 0 ||
        !decimal.TryParse(Console.ReadLine(), out var price) ||
        price < 0)
        {
            Console.WriteLine("Invalid product.");
            continue;
        }
        items.Add(new Product(name, price));
        File.WriteAllText(FileName, JsonSerializer.Serialize(items,
        new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("Saved.");
    }
    else Console.WriteLine("Unknown option.");
}
public record Product(string Name, decimal Price);

