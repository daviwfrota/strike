using AutoMapper;
using CyberStrike.Models.DAO;
using CyberStrike.Models.DTO;
using CyberStrike.Repositories;

namespace CyberStrike.Services.Impl;

public class ClientService : IClientService
{
    private IClientRepository _clientRepository;
    private IMapper _mapper;
    
    public ClientService(IMapper mapper, IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }
    public ClientDto Save(ClientDto client)
    {
        var verifyIfExists = _clientRepository.GetByEmail(client.Email);
        if (verifyIfExists != null && verifyIfExists.Active)
            throw new Exception("Esse email já está sendo utilizado.");
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

}