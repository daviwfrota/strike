using CyberStrike.Models.DAO;

namespace CyberStrike.Repositories.Impl;

public class ClientTokenRepository : IClientTokenRepository
{
    private readonly CyberContext _context;
    public ClientTokenRepository(CyberContext ctx)
    {
        _context = ctx;
    }

    public ClientToken Save(ClientToken clientToken)
    {
        var tokenSaved =
            _context.ClientTokens.FirstOrDefault(ct =>
                ct.Type == clientToken.Type && ct.Client.Id == clientToken.Client.Id);

        if (tokenSaved != null)
        {
            tokenSaved.UpdateToken(clientToken.Token);
            tokenSaved.ActivateIfIsRevoked();
            _context.ClientTokens.Update(tokenSaved);
            _context.SaveChanges();
            return tokenSaved;
        }
        
        _context.ClientTokens.Add(clientToken);
        _context.SaveChanges();
        return clientToken;
    }
}