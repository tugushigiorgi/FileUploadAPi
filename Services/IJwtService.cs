using FileUploaderApi.Data_Transfer_Objects.JwtDto_s;
using FileUploaderApi.Database.Models;

namespace FileUploaderApi.Services;

public interface IJwtService
{

    public string  CreateToken(string userid,string Email);

    public bool CheckExpiredToken(string accesToken);
    
    
    
    
    

}