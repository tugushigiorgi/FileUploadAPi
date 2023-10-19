using System.Security.Claims;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FileUploaderApi.Controllers;

[ApiController]
[Route("file")]
public class FileController:ControllerBase
{

    private IFileService _FileRepository;

    public FileController(IFileService repo)
    {
        _FileRepository = repo;

    }


        //saves file For Current Logged user 
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
    {
        if (files.IsNullOrEmpty()) return BadRequest("Select At least 1 file");
         
    var result = await  _FileRepository.SaveFiles(files,(Guid)GetLoggedUserId());

    if (result.IsSuccesfull) return Ok(result.Message);

    return BadRequest(result.Message);







    }

    //Gets File By Id 
    [HttpGet("{id}")]
    public  IActionResult  GetFileById(Guid id)
    {
        var result = _FileRepository.GetFileById(id);
        if (result != null) return Ok(result);

        return NotFound();



    }
    //Gets Current Logged user All files
    [HttpGet("all")]
    public IActionResult GetUserAllFiles()
    {
        var currentuserid = GetLoggedUserId();
        if (currentuserid == null) return BadRequest();
        
        var result = _FileRepository.GetUserAllFiles((Guid)currentuserid);

        return Ok(result);


    }

    [NonAction]
    public Guid? GetLoggedUserId()
    {
         
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return null;
            Guid.TryParse(userIdClaim.Value, out Guid userId);
            return userId;

        }
        catch (Exception ex)
        {
            return null;
        }

    }
}