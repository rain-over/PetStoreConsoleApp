namespace PetStoreConsoleApp;

public interface IPetStoreService
{
    public Task<List<Pet>> GetAvailablePets();
}
