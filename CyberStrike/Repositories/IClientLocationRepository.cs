using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories;

public interface IClientLocationRepository
{
    Task<ClientLocation> SaveAsync(ClientLocation location);
}