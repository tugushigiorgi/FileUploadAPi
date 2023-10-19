using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Data_Transfer_Objects.ControllerResponseDto_s;

namespace FileUploaderApi.Services;

public interface IFileService
{

    public Task<ControllerResponse> SaveFiles(List<IFormFile> files,Guid userid);


    public FileDto? GetFileById(Guid id);



    public  List<FileDto>  GetUserAllFiles(Guid userid);

}