using Microsoft.EntityFrameworkCore;
using StudTicketing.Database;
using StudTicketing.Services.Abstractions;

namespace StudTicketing.Services.Implementations;

using StudTicketing.Services.Abstractions;
using StudTicketing.DataTransferObjects;

public class UserService(TicketingDatabaseContext databaseContext) : IUserService

{
    public async Task AddUser(UsersAddRecord user)
    {
        // La adaugare mapam datele din obiect pe entitatea din baza de date si il atasam la context
        await databaseContext.Set<User>().AddAsync(new User()
        {
            Nume = user.Nume,
            Prenume = user.Prenume,
            Email = user.Email
        });

        // Dupa atasarea acestua salvam modificarile in baza de date, altfel nu vor fi luate in considerare.
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateUser(UsersUpdateRecord user)
    {
        // Extragem din baza de date elementul care va fi actualizat
        var entry = await databaseContext.Set<User>()
            .Where(e => e.Id == user.Id)
            .FirstOrDefaultAsync();

        if (entry == null)
        {
            return;
        }

        // Actualizam campurile
        entry.FirstName = user.FirstName;
        entry.LastName = user.LastName;
        entry.Email = user.Email;

        // (Optional) actualizam si in context entitate
        databaseContext.Set<User>().Update(entry);

        // In final trimitem modificarile catre baza de date
        await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        // Cautam elementul dupa ID in baza de date
        var entry = await databaseContext.Set<User>().Where(e => e.Id == userId).FirstOrDefaultAsync();

        if (entry == null)
        {
            return;
        }

        // Il stergem din context
        databaseContext.Set<User>().Remove(entry);
        
        // Si trimitem modificarile catre baza de date
        await databaseContext.SaveChangesAsync();
    }

    public async Task<UserRecord> GetUser(int userId)
    {
        return await databaseContext.Set<User>()
            .Where(e => e.Id == userId) // Cautam dupa ID elementul
            .Select(e => new UserRecord // Facem o proiectie si mapam elementul la un obiect de tranfer
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).FirstAsync(); // La final extragem un singur element
    }

    public Task<List<UserRecord>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserRecord>> GetUsers(int userId)
    {
        return await databaseContext.Set<User>()
            .Where(e => e.Id == userId) // Cautam dupa ID elementul
            .Select(e => new UserRecord // Facem o proiectie si mapam elementul la un obiect de tranfer
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToListAsync(); // La final extragem o lista de elemente
    }

    public async Task<PaginationResponse<UserRecord>> GetPagedUsers(PaginationQueryParams query)
    {
        return new PaginationResponse<UserRecord>()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            PageCount = await databaseContext.Set<User>().CountAsync(),
            Data = await databaseContext.Set<User>()
                .Select(e => new UserRecord()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                })
                .OrderBy(e => e.Id)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync()
        };

    }
}