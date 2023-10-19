namespace FileUploaderApi.Database.Models;

public class FileMetadata
{
    public Guid Id { get; set; }     
    
    public User user { get; set; }
    public Guid UserId { get; set; }
    public string FileName { get; set; }     
    public string PublicUrl { get; set; }     
    public string FileType { get; set; }      
    public string FileExtension { get; set; }  
    public long FileSize { get; set; }      
    public DateTime UploadDateTime { get; set; } 
}