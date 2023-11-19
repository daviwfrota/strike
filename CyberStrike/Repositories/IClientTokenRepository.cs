using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories;

public interface IClientTokenRepository
{
    ClientToken Save(ClientToken clientToken);
    ClientToken GetByTokenAndUser(string token, string email);
}