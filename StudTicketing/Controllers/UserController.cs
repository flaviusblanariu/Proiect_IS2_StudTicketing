using Microsoft.AspNetCore.Mvc;
using StudTicketing.DataTransferObjects;
using StudTicketing.Services.Abstractions;

namespace StudTicketing.Controllers;

// Clasa trebuiw sa aiba aceste atribute pentru a putea fi apelate la cererea clientilor
[ApiController]
// Aici specificam care este prefixul rutei, in acest caz va fi numele controller-ului urmat de numele metodei
[Route("[controller]/[action]")]
public class UserController(IUserService userService) : ControllerBase
{
    // Aici avem decorat cu un atribut ce indica ca se foloseste o cerere de tip POST
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UsersAddRecord user) // Atributul aici indica faptul ca parametrul este extras din corpul mesajul care este de tip JSON
    {
        await userService.AddUser(user);
        
        
        // Raspunsul va fi un raspuns gol cu status code 204 No Content
        return NoContent();
    }
    
    // Aici avem decorat cu un atribut ce indica ca se foloseste o cerere de tip PUT
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateRecord user)
    {
        await userService.UpdateUser(user);
        
        return NoContent();
    }
    
    // Aici avem decorat cu un atribut ce indica ca se foloseste o cerere de tip GET
    // Aici ruta este una variabila care la final are un ID ce va fi extras si trimis catre apelul metodei
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UsersRecord>> GetUser([FromRoute] int userId) // Atributul aici indica faptul ca parametrul este extras din ruta cererii
    {
        // Raspunsul va fi un raspuns continand datele cerute cu status code 200 Ok
        return Ok(await userService.GetUser(userId));
    }
    
    // E acelasi lucru ca metoda anterioara doar cu alt tip de date la iesire si fara alti parametri
    [HttpGet]
    public async Task<ActionResult<UsersRecord>> GetUsers()
    {
        return Ok(await userService.GetUsers());
    }
    
    // Aici avem decorat cu un atribut ce indica ca se foloseste o cerere de tip DELETE
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<UsersRecord>> DeleteUser([FromRoute] int userId)
    {
        await userService.DeleteUser(userId);
        
        return NoContent();
    }
}