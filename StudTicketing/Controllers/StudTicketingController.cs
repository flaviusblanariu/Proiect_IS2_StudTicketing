using Microsoft.AspNetCore.Mvc;

namespace StudTicketing.Controllers;

[ApiController]
[Route("[controller]")]
public class StudTicketingController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StudTicketingController> _logger;

    public StudTicketingController(ILogger<StudTicketingController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Tichete facultate")]
    public IEnumerable<StudTicketing_Run> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new StudTicketing_Run
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}