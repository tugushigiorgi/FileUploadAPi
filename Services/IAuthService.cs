using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.JwtDto_s;

namespace FileUploaderApi.Services;

public interface IAuthService
{


    public Task<JwtDto> Authenticate(LoginDto dto);


    public  Task<RefreshTokenResponse> Refreshtoken(string Accestoken, string Refreshtoken);



}