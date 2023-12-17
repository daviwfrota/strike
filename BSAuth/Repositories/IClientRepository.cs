using BSAuth.Models.DAO;

namespace BSAuth.Repositories;

public interface IClientRepository
{
    Client Add(Client user);
    Client Update(Client user);
    Client? GetById(Guid guid);
    List<Client> GetUsers();

    Client? GetByEmail(string email);
}