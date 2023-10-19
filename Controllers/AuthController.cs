using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.JwtDto_s;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploaderApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController:ControllerBase
{
    private IAuthService _authrepository;
      
    public AuthController(IAuthService authrepo)
    {
        _authrepository = authrepo;

    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto  dto)
    {
        var result = await _authrepository.Authenticate(dto);
        if (result.IsSuccesfull) return Ok(result);
        return BadRequest(result);


    }

    
    [HttpGet("refreshtoken")]
    [AllowAnonymous]
    public async Task<ActionResult> RefreshJwt([FromBody] RefreshTokenDto dto)
    {
       
        var result =await  _authrepository.Refreshtoken(dto.Token,dto.RefreshToken);
     
        
        if (result.IsSuccesfull) return Ok(result);
        



     
        return BadRequest(result.message);

    }
    
    
    
    
    
}