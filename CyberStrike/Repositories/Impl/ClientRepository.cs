using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories.Impl;

public class ClientRepository : IClientRepository
{
    private readonly CyberContext _context;
    public ClientRepository(CyberContext ctx)
    {
        _context = ctx;
    }

    public Client? GetByEmail(string email)
    {
        return _context.Clients.FirstOrDefault(user => user.Email == email);
    }

    public Client Add(Client user)
    {
        _context.Clients.Add(user);
        _context.SaveChanges();
        return user;
    }

    public Client Update(Client user)
    {
        _context.Clients.Update(user);
        _context.SaveChanges();
        return user;
    }

    public List<Client> GetUsers()
    {
        return _context.Clients.ToList();
    }

    public Client? GetById(Guid guid)
    {
        return _context.Clients.FirstOrDefault(u => u.Id == guid);
    }
}