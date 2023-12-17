namespace BSAuth.Errors;

public class InternalServerException : Exception
{
    public string Title { get; }
    public override string Message { get; }
    public int StatusCode = 500;

    public InternalServerException(string title, string message)
    {
        Title = title;
        Message = message;
    }
    
    public InternalServerException(string message)
    {
        Title = "Erro no servidor";
        Message = message;
    }
}