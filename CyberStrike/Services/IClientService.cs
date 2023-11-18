using CyberStrike.Models.DTO.Clients;
using CyberStrike.Models.DTO.Login;

namespace CyberStrike.Services;

public interface IClientService
{
    ClientDto Save(ClientDto client);
    ClientDto Update(ClientDto client);
    ClientDto Delete(ClientDto client);
    ClientDto GetByEmail(string email);

    Response Login(Request request);
}