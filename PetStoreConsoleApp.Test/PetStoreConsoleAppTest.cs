using Moq;

namespace PetStoreConsoleApp;

/* 
 * Expected Behaviors
 * 1. Return all AVAILABLE Pets from the Pet Store
 * 2. Print AVAILABLE Pets ORDER BY Categories && Name(Pet Name? Category Name?) by DESC 
 */

public class PetStoreConsoleAppTest
{
    private readonly List<Pet> _mockPets;

    public PetStoreConsoleAppTest()
    {
        _mockPets = new List<Pet>
            {
                new Pet{ Id = 1, Name = "adoggie", Category = new Category{ Id = 0, Name = "Dog"}},
                new Pet{ Id = 2, Name = "xkitty", Category = new Category{ Id = 0, Name = "Cat"}},
                new Pet{ Id = 2, Name = "ykitty", Category = new Category{ Id = 0, Name = "Cat"}},
                new Pet{ Id = 2, Name = "zkitty", Category = new Category{ Id = 0, Name = "Cat"}},
                new Pet{ Id = 1, Name = "bdoggie", Category = new Category{ Id = 0, Name = "Dog"}},
                new Pet{ Id = 1, Name = "cdoggie", Category = new Category{ Id = 0, Name = "Dog"}},
            };
    }

    [Fact]
    public async Task PetStoreService_GetAvailablePets()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePets()).ReturnsAsync(_mockPets);

        var petStoreController = new PetStoreController(mockedPets.Object);
        var pets = await petStoreController.GetAvailablePets();

        Assert.NotEmpty(pets);
    }

    [Fact]
    public async Task PetStoreService_GetAvailablePetsByCategory()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePetsByCategory()).ReturnsAsync(_mockPets);

        var petStoreController = new PetStoreController(mockedPets.Object);
        var pets = await petStoreController.GetAvailablePetsByCategory();

        Assert.NotEmpty(pets);
        //assert sorting
    }

    [Fact]
    public async Task PetStoreService_GetAvailablePets_ThrowsExceptionOnError()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePets()).ThrowsAsync(new Exception("Error"));

        var petStoreController = new PetStoreController(mockedPets.Object);

        await Assert.ThrowsAsync<Exception>(() => petStoreController.GetAvailablePets());
    }

    [Fact]
    public async Task PetStoreService_GetAvailablePetsByCategory_ThrowsExceptionOnError()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePetsByCategory()).ThrowsAsync(new Exception("Error"));

        var petStoreController = new PetStoreController(mockedPets.Object);

        await Assert.ThrowsAsync<Exception>(() => petStoreController.GetAvailablePetsByCategory());
    }
}
