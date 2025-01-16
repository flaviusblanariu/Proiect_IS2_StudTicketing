using Microsoft.AspNetCore.Mvc;
using StudTicketing.Database.Models;
using System.Collections.Generic;

namespace StudTicketing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudTicketingController : ControllerBase
    {
        private static readonly string[] Departments = new[]
        {
            "Studenti", "Profesori", "Secretariat", "Decanat", "Camin"
        };

        private readonly ILogger<StudTicketingController> _logger;

        public StudTicketingController(ILogger<StudTicketingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTickets")]
        public IEnumerable<Ticket> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Ticket
                {
                    Id = index,
                    Title = $"Ticket {index}",
                    Department = Departments[Random.Shared.Next(Departments.Length)],
                    Description = "Descriere ticket",
                    Status = (TicketStatus)Random.Shared.Next(0, 4),
                    CreatedDate = DateTime.Now.AddDays(-index),
                    UpdatedDate = DateTime.Now
                })
                .ToArray();
        }
    }
}