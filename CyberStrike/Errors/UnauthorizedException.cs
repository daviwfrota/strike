namespace CyberStrike.Errors;

public class UnauthorizedException : Exception
{
    public string Title { get; }
    public override string Message { get; }
    public int StatusCode = 401;

    public UnauthorizedException(string title, string message)
    {
        Title = title;
        Message = message;
    }
    
    public UnauthorizedException(string message)
    {
        Title = "Dados inválidos";
        Message = message;
    }
}