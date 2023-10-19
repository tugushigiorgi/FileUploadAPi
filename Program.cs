using FileUploaderApi.Database;
using FileUploaderApi.Database.Models;
using FileUploaderApi.Repositories;
using FileUploaderApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);

//Uncomment IF u use Frontend framework(React,Vue,Angular...) and specify that port 
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("Policy",
//         policy =>
//         {
//             policy.WithOrigins("https://localhost:5166", "http://localhost:5166");
//             policy.SetIsOriginAllowed(origin => true);
//             policy.AllowAnyOrigin();
//             policy.AllowAnyHeader();
//             policy.AllowAnyMethod();
//         });
// });



//repositories

builder.Services.AddScoped<IFileService, FIleRepository>();
builder.Services.AddScoped<IJwtService, JwtRepository>();
builder.Services.AddScoped<IAuthService, AuthRepository>();
builder.Services.AddScoped<UserRepository>();
//end of repositories 


// Authentication Service For Jwt 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:AUDIANCE"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:ISSUER"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))            
        };



    }

);



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