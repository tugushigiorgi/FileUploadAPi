using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.ControllerResponseDto_s;
using FileUploaderApi.Database;
using FileUploaderApi.Database.Models;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Identity;

namespace FileUploaderApi.Repositories;

public class FIleRepository : IFileService
{

    private FileDbContext _dbContext;
    private IConfiguration _configuration;
    private UserManager<User> _userManager;
    public FIleRepository(FileDbContext db, IConfiguration _config,UserManager<User> usrmanager)
    {
        _dbContext = db;
        _configuration = _config;
        _userManager = usrmanager;


    }



    public async Task<ControllerResponse> SaveFiles(List<IFormFile> files,Guid userid)
    {
        var curentuser = await _userManager.FindByIdAsync(userid.ToString());
        var uploadLocation = _configuration["FileUploadURL"];
        
        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadLocation!, uniqueFileName);
                try
                {
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception e)
                {
                    return new ControllerResponse
                    {
                        IsSuccesfull = false, Message = $"Error While Coping file to the Directory :{uploadLocation}"
                    };
                }



                var fileInformation = new FileMetadata
                {
                    user = curentuser!,
                    FileName = file.FileName,
                    PublicUrl = $"/{uploadLocation}/{uniqueFileName}",
                    FileType = file.ContentType,
                    FileExtension = Path.GetExtension(file.FileName),
                    FileSize = file.Length,
                    UploadDateTime = DateTime.UtcNow,
                };




                 
                    _dbContext.FileMetadatas.Add(fileInformation);

                   
                
            }



            

        }

        try
        {
            await _dbContext.SaveChangesAsync();
            
        }
        catch (Exception e)
        {
            return new ControllerResponse
            {
                IsSuccesfull = false, Message = "Error While Saving in Database"
            };
        }
        
        
        return new ControllerResponse
        {
            IsSuccesfull = true, Message = "File(s) Saved Succesfully"
        };
        
       

    }

    public FileDto? GetFileById(Guid id)
    {
        var currentfile = _dbContext.FileMetadatas.SingleOrDefault(f => f.Id == id);
        if (currentfile == null) return null;

        var dto = new FileDto
        {
            Id = currentfile.Id.ToString(),
            FileName = currentfile.FileName,
            FileSize = currentfile.FileSize,
            FileType = currentfile.FileType,
            FileExtension = currentfile.FileExtension,
            PublicUrl = currentfile.PublicUrl,
            UploadDateTime = currentfile.UploadDateTime
            
            
            
        };
        return dto;


    }

    public  List<FileDto>  GetUserAllFiles(Guid userid)
    {
   
        var files = _dbContext.FileMetadatas.Where(usr => usr.UserId == userid)
            .Select(currentfile =>
                new FileDto
                {
                    Id = currentfile.Id.ToString(),
                    FileName = currentfile.FileName,
                    FileSize = currentfile.FileSize,
                    FileType = currentfile.FileType,
                    FileExtension = currentfile.FileExtension,
                    PublicUrl = currentfile.PublicUrl,
                    UploadDateTime = currentfile.UploadDateTime

                }




            ).ToList();


        return files;

    }
}
 