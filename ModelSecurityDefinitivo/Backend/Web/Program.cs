using Entity.Context;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Utilities.Notification.Email;
using Web.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddProjectServices();
builder.Services.AddCorsConfiguration(builder.Configuration);

// DbContext dinámico según el motor
builder.Services.AddCustomDataBase(builder.Configuration);

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
// security-459715