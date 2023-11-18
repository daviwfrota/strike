using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories.Impl;

public class ClientLocationRepository : IClientLocationRepository
{
    private readonly CyberContext _context;
    public ClientLocationRepository(CyberContext ctx)
    {
        _context = ctx;
    }

    public async Task<ClientLocation> SaveAsync(ClientLocation location)
    {
        await _context.ClientLocations.AddAsync(location);
        await _context.SaveChangesAsync();
        return location;
    }
}