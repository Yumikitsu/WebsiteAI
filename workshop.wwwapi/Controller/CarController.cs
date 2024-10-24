using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private static List<Car> cars = new List<Car>
    {
        new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020 },
        new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2019 },
        new Car { Id = 3, Make = "Ford", Model = "Focus", Year = 2018 }
    };

    [HttpGet]
    public IEnumerable<Car> Get()
    {
        return cars;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Car car)
    {
        car.Id = cars.Max(c => c.Id) + 1;
        cars.Add(car);
        return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Car updatedCar)
    {
        var car = cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        car.Make = updatedCar.Make;
        car.Model = updatedCar.Model;
        car.Year = updatedCar.Year;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var car = cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        cars.Remove(car);
        return NoContent();
    }
}

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}
