using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Animal;

public class CreateAnimalDTO
{
    [Required]
    public string Name { get; set; }
}

public class UpdateAnimalDTO
{
    public string Name { get; set; }
}