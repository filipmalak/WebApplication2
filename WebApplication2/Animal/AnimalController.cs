using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Animal;

[Route("/api/animal")]
[ApiController]
public class AnimalController : ControllerBase
{
    private IAnimalService _animalService;
    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.CreateAnimal(dto);
        return success ? Created() : Conflict();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.AddAnimal(dto);

        if (success)
        {
            return CreatedAtAction(nameof(GetAnimals), new { id = dto.Name }, dto);
        }
        else
        {
            return Conflict();
        }
    }
    
    [HttpPut("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] UpdateAnimalDTO dto)
    {
        var success = _animalService.UpdateAnimal(idAnimal, dto);

        if (success)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var success = _animalService.DeleteAnimal(idAnimal);

        if (success)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }


}