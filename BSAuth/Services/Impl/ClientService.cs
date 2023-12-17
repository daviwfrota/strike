using System.Security.Claims;
using AutoMapper;
using BSAuth.Constants;
using BSAuth.Errors;
using BSAuth.Models.DAO;
using BSAuth.Models.DTO.Clients;
using BSAuth.Models.DTO.Login;
using BSAuth.Models.Enums;
using BSAuth.Repositories;
using BSAuth.Utils;
using Microsoft.Extensions.Options;

namespace BSAuth.Services.Impl;

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
            
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, client.Email)  };
            var jwt = new GenerateJwt(_security.ExpireIn, _security.Secret, claims);
            _clientTokenRepository.Save(new ClientToken(jwt.Jwt, TokenType.ACTIVATE_ACCOUNT, userToSave));
            
            /*
             * TODO
             * send to rabbitmq queue client email with verification code
             */
            
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
            if (!client.IsVerified)
                throw new UnauthorizedException("Essa conta ainda não foi verificada.");

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
            
            /*
             * TODO
             * send to rabbitmq queue queue client is connected
             */
            
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerException(e.Message);
        }
    }

    public Response RefreshToken(RefreshTokenDto refreshToken)
    {
        try
        {
            var token = new DecodeJwt(refreshToken.Token);
            if(!token.isValid)
                throw new UnauthorizedException("Refresh token inválido.");

            var ct = _clientTokenRepository.GetByTokenAndUser(refreshToken.Token, token.Email);
            if (ct == null)
                throw new UnauthorizedException("Refresh token inválido.");

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, ct.Client.Email)  };
            var jwt = new GenerateJwt(_security.ExpireIn, _security.Secret, claims);
            var response = new Response(jwt.Jwt, "Bearer", refreshToken.Token);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerException(e.Message);
        }
    }

    public Response ActiveAccount(string token)
    {
        try
        {
            var tokenDecoded = new DecodeJwt(token);
            if(!tokenDecoded.isValid)
                throw new UnauthorizedException("Esse token é inválido");
            var ct = _clientTokenRepository.GetByTokenAndUser(token, tokenDecoded.Email);
            if(ct == null)
                throw new UnauthorizedException("Esse token é inválido");
        
            var client = _clientRepository.GetByEmail(tokenDecoded.Email);
            if (client == null)
                throw new BadRequestException("Cliente não existe.");
            client.Verify();
            _clientRepository.Update(client);
            
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, ct.Client.Email)  };
            var jwt = new GenerateJwt(_security.ExpireIn, _security.Secret, claims);
            var response = new Response(jwt.Jwt, "Bearer");
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerException(e.Message);
        }
    }
}