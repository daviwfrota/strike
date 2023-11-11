using CyberStrike.Models.DTO;

namespace CyberStrike.Services;

public interface IClientService
{
    ClientDto Save(ClientDto client);
    ClientDto Update(ClientDto client);
    ClientDto Delete(ClientDto client);
    ClientDto GetByEmail(string email);
}