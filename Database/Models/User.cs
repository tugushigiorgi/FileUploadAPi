using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FileUploaderApi.Database.Models;

public class User:IdentityUser<Guid>
{
    [JsonIgnore]
    public List<FileMetadata> files { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExp { get; set; }
    
    
}