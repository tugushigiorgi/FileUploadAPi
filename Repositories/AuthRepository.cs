using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.JwtDto_s;
using FileUploaderApi.Database;
using FileUploaderApi.Database.Models;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Identity;

namespace FileUploaderApi.Repositories;

public class AuthRepository:IAuthService
{
    private IJwtService jwtrepository;
    private UserManager<User> _UserManager;
    private IConfiguration _configuration;
    private FileDbContext db;
    
    public AuthRepository(IJwtService jwtrepo,UserManager<User> usrmanager,IConfiguration config,FileDbContext database)

    {
        jwtrepository = jwtrepo;
        _UserManager = usrmanager;
        _configuration = config;
        db = database;

    }





    public async Task<JwtDto> Authenticate(LoginDto dto)
    {
        var getuser = await  _UserManager.FindByEmailAsync(dto.Email);
        if (getuser == null)
        {
            return new JwtDto {IsSuccesfull = false, message = "User Not Found With Given Email Address"};

        }

        var checkpassword =await  _UserManager.CheckPasswordAsync(getuser, dto.Password);

        if (!checkpassword)
        {
            return new JwtDto {IsSuccesfull = false, message = "Password is Incorrect"};

        }
        
        var token = jwtrepository.CreateToken(getuser.Id.ToString(),getuser.Email);

        var refreshtoken = Guid.NewGuid().ToString();
        getuser.RefreshToken = refreshtoken;
        getuser.RefreshTokenExp=DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:REFRESH_TOKEN_EXPIRATION_DAYS"]));

        await db.SaveChangesAsync();
        return new JwtDto {IsSuccesfull = true, message = "Succesfull login",Token = token,RefreshToken = refreshtoken};

       
        
        
        
    }

    public async Task<RefreshTokenResponse> Refreshtoken(string Accestoken, string Refreshtoken)
    {

        var checktoken = jwtrepository.CheckExpiredToken(Accestoken);
        if(!checktoken) return new RefreshTokenResponse { IsSuccesfull = false, message = "Invalid Access token" };


        var user = db.Users.SingleOrDefault(usr => usr.RefreshToken == Refreshtoken);
        if (user == null) return new RefreshTokenResponse { IsSuccesfull = false,message = "Invalid Refresh token"};

        if ( user.RefreshTokenExp >= DateTime.Now)
        {
            var newtoken = jwtrepository.CreateToken(user.Id.ToString(),user.Email!);
            var newrefresh = Guid.NewGuid().ToString();
            user.RefreshToken = newrefresh;
            user.RefreshTokenExp =
                DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:REFRESH_TOKEN_EXPIRATION_DAYS"]));
            await db.SaveChangesAsync();
            return new RefreshTokenResponse  {IsSuccesfull=true, Token = newtoken, RefreshToken = newrefresh };

        }
        
        return new RefreshTokenResponse{IsSuccesfull = false};

        
        












    }
}