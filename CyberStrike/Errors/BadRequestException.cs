namespace CyberStrike.Errors;

public class BadRequestException : Exception
{
    public string Title { get; }
    public override string Message { get; }
    public int StatusCode = 404;

    public BadRequestException(string title, string message)
    {
        Title = title;
        Message = message;
    }
    
    public BadRequestException(string message)
    {
        Title = "Dados inválidos";
        Message = message;
    }
}