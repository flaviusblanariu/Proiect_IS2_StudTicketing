using Microsoft.EntityFrameworkCore;
using StudTicketing.Database;
using StudTicketing.Database.Models;
using StudTicketing.Services.Abstractions;

namespace StudTicketing.Services.Implementations;

using StudTicketing_Run.Services.Abstractions;
using StudTicketing_Run.DataTransferObjects;

public class UserService(TicketingDatabaseContext databaseContext) : IUserService

{
    public async Task AddUser(UsersAddRecord user)
    {
        // La adaugare mapam datele din obiect pe entitatea din baza de date si il atasam la context
        await databaseContext.Set<Users>().AddAsync(new Users()
        {
            Nume = user.Nume,
            Prenume = user.Prenume,
            Functie = user.Functie,
            Telefon = user.Telefon,
            Email = user.Email
        });

        // Dupa atasarea acestua salvam modificarile in baza de date, altfel nu vor fi luate in considerare.
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateUser(UsersUpdateRecord user)
    {
        // Extragem din baza de date elementul care va fi actualizat
        var entry = await databaseContext.Set<Users>()
            .Where(e => e.id == user.id)
            .FirstOrDefaultAsync();

        if (entry == null)
        {
            return;
        }

        // Actualizam campurile
        entry.Nume = user.Nume;
        entry.Prenume = user.Prenume;
        entry.Functie = user.Functie;
        entry.Telefon = user.Telefon;
        entry.Email = user.Email;

        // (Optional) actualizam si in context entitate
        databaseContext.Set<Users>().Update(entry);

        // In final trimitem modificarile catre baza de date
        await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        // Cautam elementul dupa ID in baza de date
        var entry = await databaseContext.Set<Users>().Where(e => e.id == userId).FirstOrDefaultAsync();

        if (entry == null)
        {
            return;
        }

        // Il stergem din context
        databaseContext.Set<Users>().Remove(entry);
        
        // Si trimitem modificarile catre baza de date
        await databaseContext.SaveChangesAsync();
    }

    public async Task<UsersRecord> GetUser(int userId)
    {
        return await databaseContext.Set<Users>()
            .Where(e => e.id == userId) // Cautam dupa ID elementul
            .Select(e => new UsersRecord // Facem o proiectie si mapam elementul la un obiect de tranfer
            {
                id = e.id,
                Nume = e.Nume,
                Prenume = e.Prenume,
                Functie = e.Functie,
                Telefon = e.Telefon,
                Email = e.Email
                
            }).FirstAsync(); // La final extragem un singur element
    }

    public Task<List<UsersRecord>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<List<UsersRecord>> GetUsers(int userId)
    {
        return await databaseContext.Set<Users>()
            .Where(e => e.id == userId) // Cautam dupa ID elementul
            .Select(e => new UsersRecord // Facem o proiectie si mapam elementul la un obiect de tranfer
            {
                id = e.id,
                Nume = e.Nume,
                Prenume = e.Prenume,
                Functie = e.Functie,
                Telefon = e.Telefon,
                Email = e.Email
            }).ToListAsync(); // La final extragem o lista de elemente
    }

    public async Task<PaginationResponse<UsersRecord>> GetPagedUsers(PaginationQueryParams query)
    {
        return new PaginationResponse<UsersRecord>()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            PageCount = await databaseContext.Set<Users>().CountAsync(),
            Data = await databaseContext.Set<Users>()
                .Select(e => new UsersRecord()
                {
                    id = e.id,
                    Nume = e.Nume,
                    Prenume = e.Prenume,
                    Functie = e.Functie,
                    Telefon = e.Telefon,
                    Email = e.Email
                })
                .OrderBy(e => e.id)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync()
        };

    }
}