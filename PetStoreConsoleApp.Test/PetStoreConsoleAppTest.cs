using Moq;
using System.Collections;

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
    public void PetStoreService_GetAvailablePets()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePets()).Returns(_mockPets);

        var petStoreController = new PetStoreController(mockedPets.Object);
        var pets = petStoreController.GetAvailablePets();

        Assert.NotEmpty(pets);
    }

    [Fact]
    public void PetStoreService_GetAvailablePetsByCategory()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePetsByCategory()).Returns(_mockPets);

        var petStoreController = new PetStoreController(mockedPets.Object);
        var pets = petStoreController.GetAvailablePetsByCategory();

        Assert.NotEmpty(pets);
        //assert sorting
    }

    [Fact]
    public void PetStoreService_GetAvailablePets_ThrowsExceptionOnError()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePets()).Throws(new Exception("Error"));

        var petStoreController = new PetStoreController(mockedPets.Object);

        Assert.Throws<Exception>(() => petStoreController.GetAvailablePets());
    }

    [Fact]
    public void PetStoreService_GetAvailablePetsByCategory_ThrowsExceptionOnError()
    {
        var mockedPets = new Mock<IPetStoreService>();
        mockedPets.Setup(svc => svc.GetAvailablePetsByCategory()).Throws(new Exception("Error"));

        var petStoreController = new PetStoreController(mockedPets.Object);

        Assert.Throws<Exception>(() => petStoreController.GetAvailablePetsByCategory());
    }
}
public class PetStoreController
{
    private IPetStoreService _petStoreService;

    public PetStoreController(IPetStoreService petStoreService)
    {
        _petStoreService = petStoreService;
    }

    public List<Pet> GetAvailablePets()
    {
        return _petStoreService.GetAvailablePets();
    }

    public List<Pet> GetAvailablePetsByCategory()
    {
        return _petStoreService.GetAvailablePetsByCategory();
    }
}

public interface IPetStoreService
{
    public List<Pet> GetAvailablePets();
    public List<Pet> GetAvailablePetsByCategory();
}

public class Pet
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
}