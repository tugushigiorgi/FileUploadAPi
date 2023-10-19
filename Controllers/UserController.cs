using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.ControllerResponseDto_s;
using FileUploaderApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploaderApi.Controllers;
[ApiController]
[Route("user")]
public class UserController:ControllerBase
{

    private UserRepository _userRepository;


    public UserController(UserRepository userRepository)
    {

        _userRepository = userRepository;

    }



    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody]  RegisterDto dto)
    {

        var result = await _userRepository.Register(dto);

        if (result.IsSuccesfull) return Ok(result.Message);
        
        
        return BadRequest(result.Message);

    }


}