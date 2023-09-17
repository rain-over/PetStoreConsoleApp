using Newtonsoft.Json;

namespace PetStoreConsoleApp;

internal class PetStoreService : IPetStoreService
{
    private readonly HttpClient _httpClient;

    public PetStoreService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
    }
    public async Task<List<Pet>> GetAvailablePets()
    {
        var response = await _httpClient.GetAsync("pet/findByStatus?status=available");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Pet> pets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            return pets;
        }
        else
        {
            throw new Exception("Failed to fetch pets from the API.");
        }

    }
}
