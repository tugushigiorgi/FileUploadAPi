using System.ComponentModel.DataAnnotations;

namespace FileUploaderApi.Data_Transfer_Objects;

public class LoginDto
{   
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
    
    
    
}