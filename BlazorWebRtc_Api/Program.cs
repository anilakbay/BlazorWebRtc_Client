using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped(typeof(BaseResponseModel));
builder.Services.AddScoped<IAccountService, BlazorWebRtc_Application.Services.AccountService>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(BlazorWebRtc_Application.Features.Commands.Account.Register.RegisterHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(BlazorWebRtc_Application.Interface.Services.IAccountService).Assembly);
}


);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
