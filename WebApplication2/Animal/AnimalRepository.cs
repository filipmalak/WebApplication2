using System.Data.SqlClient;

namespace WebApplication2.Animal;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string name);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto);

    public bool DeleteAnimal(int idAnimal);
}

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        using var command = new SqlCommand("SELECT IdAnimal, Name FROM Animals ORDER BY @orderBy", connection);
        command.Parameters.AddWithValue("orderBy", orderBy);

        var animals = new List<Animal>();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var animal = new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!
            };

            animals.Add(animal);
        }

        return animals;
    }

    public bool CreateAnimal(string name)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Animals (Name) VALUES (@name)", connection);
        command.Parameters.AddWithValue("name", name);

        return command.ExecuteNonQuery() == 1;
    }
    
    public bool AddAnimal(CreateAnimalDTO dto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
    
        using var command = new SqlCommand("INSERT INTO Animals (Name) VALUES (@name)", connection);
        command.Parameters.AddWithValue("name", dto.Name);

        return command.ExecuteNonQuery() == 1;
    }
    
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
    
        using var command = new SqlCommand("UPDATE Animals SET Name = @name WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("idAnimal", idAnimal);

        return command.ExecuteNonQuery() == 1;
    }

    public bool DeleteAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("defaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("DELETE FROM Animals WHERE idAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);

        return command.ExecuteNonQuery() == 1;
    }

}