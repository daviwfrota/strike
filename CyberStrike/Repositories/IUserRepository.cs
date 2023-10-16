using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories;

public interface IUserRepository
{
    User Add(User user);
    User Update(User user);
    User? GetById(Guid guid);
    List<User> GetUsers();
}