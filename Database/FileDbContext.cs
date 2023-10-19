using FileUploaderApi.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileUploaderApi.Database;

public class FileDbContext :IdentityDbContext<User,UserRole,Guid>
{
        public DbSet<FileMetadata> FileMetadatas { get; set; }
        


    public FileDbContext(DbContextOptions options) : base(options){}
    
    
    






}