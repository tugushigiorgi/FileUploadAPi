using FileUploaderApi.Database;
using FileUploaderApi.Database.Models;
using FileUploaderApi.Repositories;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);

//repositories

builder.Services.AddScoped<IFileService, FIleRepository>();




builder.Services.AddIdentity<User, UserRole>(options=>
    {
    options.Password.RequiredLength = 6;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequireUppercase = false;
options.Password.RequireLowercase = true;
options.Password.RequireDigit = true;

    }
        )
    .AddEntityFrameworkStores<FileDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<User, UserRole, FileDbContext, Guid>>()
    .AddRoleStore<RoleStore<UserRole, FileDbContext, Guid>>();




var app = builder.Build();

app.MapControllers();


app.Run();