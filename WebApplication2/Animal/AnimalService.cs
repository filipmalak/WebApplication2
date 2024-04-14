namespace WebApplication2.Animal;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAnimals(string orderBy);
    public bool CreateAnimal(CreateAnimalDTO dto);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int idAnimal);


}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }

    public bool CreateAnimal(CreateAnimalDTO dto)
    {
        return _animalRepository.CreateAnimal(dto.Name);
    }
    
    public bool AddAnimal(CreateAnimalDTO dto)
    {
        return _animalRepository.AddAnimal(dto);
    }
    
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto)
    {
        return _animalRepository.UpdateAnimal(idAnimal, dto);
    }

    public bool DeleteAnimal(int idAnimal)
    {
        return _animalRepository.DeleteAnimal(idAnimal);
    }
}