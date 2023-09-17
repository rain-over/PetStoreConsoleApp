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
        return await _petStoreService.GetAvailablePetsByCategory();
    }
}
