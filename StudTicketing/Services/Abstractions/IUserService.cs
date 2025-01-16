using StudTicketing.DataTransferObjects;
namespace StudTicketing.Services.Abstractions;

public interface IUserService
{
    public Task AddUser(UsersAddRecord user); // Adaugare date
    public Task UpdateUser(UsersUpdateRecord user); // Modificare date
    public Task DeleteUser(int userId); // Stergere dupa ID din baza de date
    public Task<UsersRecord> GetUser(int userId); // Extragere cu ID din baza de date
    public Task<List<UsersRecord>> GetUsers(); // Extragere a tuturor datelor din baza de date

}