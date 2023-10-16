using CyberStrike.Models.DAO;
using CyberStrike.Repository;

namespace CyberStrike.Repositories.Impl;

public class UserRepository : IUserRepository
{
    private readonly CyberContext _context;
    public UserRepository(CyberContext ctx)
    {
        _context = ctx;
    }

    public User Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetById(Guid guid)
    {
        return _context.Users.FirstOrDefault(u => u.Id == guid);
    }
}