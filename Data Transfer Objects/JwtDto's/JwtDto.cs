namespace FileUploaderApi.Data_Transfer_Objects.JwtDto_s;

public class JwtDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    
   public bool IsSuccesfull { get; set; }
   public string message { get; set; }
   
   
}