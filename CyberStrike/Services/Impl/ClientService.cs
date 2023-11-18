using System.Security.Claims;
using AutoMapper;
using CyberStrike.Constants;
using CyberStrike.Errors;
using CyberStrike.Models.DAO;
using CyberStrike.Models.DTO.Clients;
using CyberStrike.Models.DTO.Login;
using CyberStrike.Models.Enums;
using CyberStrike.Repositories;
using CyberStrike.Utils;
using Microsoft.Extensions.Options;

namespace CyberStrike.Services.Impl;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientLocationRepository _clientLocationRepository;
    private readonly IClientTokenRepository _clientTokenRepository;
    private readonly IMapper _mapper;
    private readonly Security _security;
    
    public ClientService(IMapper mapper, IOptions<Security> securityOptions, 
        IClientRepository clientRepository, 
        IClientLocationRepository clientLocationRepository,
        IClientTokenRepository clientTokenRepository)
    {
        _mapper = mapper;
        _security = securityOptions.Value;
        
        _clientRepository = clientRepository;
        _clientLocationRepository = clientLocationRepository;
        _clientTokenRepository = clientTokenRepository;
    }
    public ClientDto Save(ClientDto client)
    {
        try
        {
            var verifyIfExists = _clientRepository.GetByEmail(client.Email);
            if (verifyIfExists != null && verifyIfExists.Active)
                throw new BadRequestException("Este email já está sendo utilizado!");
            if (verifyIfExists != null && !verifyIfExists.Active && verifyIfExists.VerifyPassword(client.Password))
            {
                verifyIfExists.Activate();
                _clientRepository.Update(verifyIfExists);
                return _mapper.Map<ClientDto>(verifyIfExists);
            }

            var userToSave = _mapper.Map<Client>(client);
            userToSave.Hash();
            _clientRepository.Add(userToSave);
            return _mapper.Map<ClientDto>(userToSave);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerException(e.Message);
        }
    }

    public ClientDto Delete(ClientDto client)
    {
        throw new NotImplementedException();
    }

    public ClientDto Update(ClientDto client)
    {
        throw new NotImplementedException();
    }

    public ClientDto GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Response Login(Request request)
    {

        try
        {
            var client = _clientRepository.GetByEmail(request.Email);
            if (client == null || !client.VerifyPassword(request.Password))
                throw new BadRequestException("Email e/ou senha inválido(as).");

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, client.Email)  };
            var jwt = new GenerateJwt(_security.ExpireIn, _security.Secret, claims);
            
            var response = new Response(jwt.Jwt, "Bearer");
       
            if (request.KeepAlive)
            {
                var token = new GenerateJwt(_security.RefreshTokenExpireIn, _security.Secret, claims);
                _clientTokenRepository.Save(new ClientToken(token.Jwt, TokenType.REFRESH, client));
                response.AddRefresh(token.Jwt);
            }
            
            client.UpdateLastCommunication();
            _clientRepository.Update(client);
            
            _clientLocationRepository.SaveAsync(new ClientLocation(request.Latitude, request.Logintude, client));
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerException(e.Message);
        }

    }
}