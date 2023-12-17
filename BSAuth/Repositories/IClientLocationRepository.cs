using BSAuth.Models.DAO;

namespace BSAuth.Repositories;

public interface IClientLocationRepository
{
    Task<ClientLocation> SaveAsync(ClientLocation location);
}