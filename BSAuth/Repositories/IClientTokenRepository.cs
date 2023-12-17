using BSAuth.Models.DAO;

namespace BSAuth.Repositories;

public interface IClientTokenRepository
{
    ClientToken Save(ClientToken clientToken);
    ClientToken GetByTokenAndUser(string token, string email);
}