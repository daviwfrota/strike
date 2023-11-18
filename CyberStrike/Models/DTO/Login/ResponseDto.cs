namespace CyberStrike.Models.DTO.Login;

public class Response
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Type { get; set; }

    public Response(string accessToken, string type, string refreshToken)
    {
        AccessToken = accessToken;
        Type = type;
        RefreshToken = refreshToken;
    }
    
    public Response(string accessToken, string type)
    {
        AccessToken = accessToken;
        Type = type;
    }
}