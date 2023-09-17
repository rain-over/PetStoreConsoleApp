namespace PetStoreConsoleApp;

public class PetStoreController
{
    private IPetStoreService _petStoreService;

    public PetStoreController(IPetStoreService petStoreService)
    {
        _petStoreService = petStoreService;
    }

    public async Task<List<Pet>> GetAvailablePets()
    {
        return await _petStoreService.GetAvailablePets();
    }

    public async Task<List<Pet>> GetAvailablePetsByCategory()
    {
        List<Pet> pets = await _petStoreService.GetAvailablePets();
        pets.ForEach(p =>
        {
            p.Name = p.Name?.ToLower();
            if (p.Category != null)
            {
                p.Category.Name = p?.Category?.Name.ToLower();
            }

        });

        return pets.OrderBy(p => p.Category?.Name).ThenByDescending(p => p.Name).ToList();
    }
}
