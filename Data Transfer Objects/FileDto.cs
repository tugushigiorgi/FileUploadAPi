namespace FileUploaderApi.Data_Transfer_Objects;

public class FileDto
{
    public int Id { get; set; }     
    public string FileName { get; set; }     
    public string PublicUrl { get; set; }     
    public string FileType { get; set; }      
    public string FileExtension { get; set; }  
    public long FileSize { get; set; }      
    public DateTime UploadDateTime { get; set; } 
}