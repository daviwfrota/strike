using BSAuth.Models.DTO.Clients;
using BSAuth.Models.DTO.Login;

namespace BSAuth.Services;

public interface IClientService
{
    ClientDto Save(ClientDto client);
    ClientDto Update(ClientDto client);
    ClientDto Delete(ClientDto client);
    ClientDto GetByEmail(string email);

    Response Login(Request request);
    Response RefreshToken(RefreshTokenDto refreshToken);
    Response ActiveAccount(string token);
}