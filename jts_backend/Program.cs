using jts_backend.Context;
using jts_backend.Services.AuthService;
using jts_backend.Services.DepartmentService;
using jts_backend.Services.FileService;
using jts_backend.Services.JobTitleService;
using jts_backend.Services.PriorityService;
using jts_backend.Services.RoleService;
using jts_backend.Services.StatusService;
using jts_backend.Services.TicketService;
using jts_backend.Services.UserService;
using jts_backend.Services.ViewService;
using jts_backend.Services.RoleManagerService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using jts_backend.Services.EmailService;
using jts_backend.Configuration;
using jts_backend.Hub;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JtsContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IUserService, UserSevice>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IJobTitleService, JobTitleService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IViewService, ViewService>();
builder.Services.AddScoped<IRoleManagerService, RoleManagerService>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Description =
                """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("AppSettings:Token").Value!
                )
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));
app.UseCors(
    x =>
        x.WithOrigins(builder.Configuration.GetSection("AppSettings:ClientUrl").Value!)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
);

app.Use(
    async (context, next) =>
    {
        context.Request.EnableBuffering();
        await next();
    }
);

app.UseAuthorization();
app.MapHub<JtsHub>("/jts-hub");
app.MapControllers();
app.Run();
