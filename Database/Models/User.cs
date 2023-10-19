using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FileUploaderApi.Database.Models;

public class User:IdentityUser<Guid>
{
    [JsonIgnore]
    public List<FileMetadata> files { get; set; }
    
    
    
    
}