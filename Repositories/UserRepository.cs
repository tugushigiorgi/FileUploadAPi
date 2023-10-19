using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.ControllerResponseDto_s;
using FileUploaderApi.Database;
using FileUploaderApi.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace FileUploaderApi.Repositories;

public class UserRepository
{
    
    private FileDbContext db;
    private UserManager<User> _userManager; 
    public UserRepository(FileDbContext database,UserManager<User> userManager)
    {
        db = database;
        _userManager = userManager;
    }

    public async Task<ControllerResponse>  Register(RegisterDto dto)
    {
        var checkEmail = await  _userManager.FindByEmailAsync(dto.Email);
        if (checkEmail != null)
        {
            return new ControllerResponse { IsSuccesfull = false, Message = "Use Different Email Address" };
        }
 
        var newuser = new User
        {
            UserName = dto.Username,
            

            Email = dto.Email,
         
        };

        var result = await _userManager.CreateAsync(newuser, dto.Password);

        if (result.Succeeded)
        {

            return new ControllerResponse { IsSuccesfull = true, Message = "User Registered Succesfully" };


        }

        string errorsdata = "";
        foreach (var error in result.Errors)
        {
            errorsdata += error.Code;
        }

        return new ControllerResponse { IsSuccesfull = false, Message = "Error: " + errorsdata };

        
        
        
        
    }







}