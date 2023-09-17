using PetStoreConsoleApp;

var petStoreService = new PetStoreService();
var petStoreController = new PetStoreController(petStoreService);

try
{
    var pets = await petStoreController.GetAvailablePetsByCategory();

    Console.WriteLine("Available Pets:");
    foreach (var pet in pets)
    {
        Console.WriteLine($"Category: {pet.Category?.Name}, Name: {pet.Name}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}