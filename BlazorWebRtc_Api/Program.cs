using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.UserFriend;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Features.Queries.UserFriend;
using BlazorWebRtc_Application.Features.Queries.UserInfo;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Application.Services;
using BlazorWebRtc_Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddScoped(typeof(BaseResponseModel));
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRequestService, RequestService>();
        builder.Services.AddScoped<IUserFriendService, UserFriendService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UserListHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(RequestHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(RequestsHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UserFriendHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UserFriendListQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(SendMessageHandler).Assembly);
        }

        );
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddSwaggerGen();

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("SecretKey");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                ValidAudience = jwtSettings.GetValue<string>("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorApp",
                policy =>
                {
                    policy.WithOrigins("https://localhost:7101/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors("AllowBlazorApp");
        app.MapControllers();

        app.Run();
    }
}