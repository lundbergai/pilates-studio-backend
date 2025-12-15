using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PilatesStudio.Api.Middleware;
using PilatesStudio.Application.Interfaces;
using PilatesStudio.Application.Services;
using PilatesStudio.Infrastructure.Persistence;
using PilatesStudio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PilatesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IClassTypesRepository, ClassTypesRepository>();
builder.Services.AddScoped<IScheduledClassRepository, ScheduledClassRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Clerk:Issuer"];
    options.Audience = builder.Configuration["Clerk:Audience"];
    
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            if (context.Principal?.Identity is ClaimsIdentity identity)
            {
                if (identity.FindFirst("admin")?.Value == "true")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));

                if (identity.FindFirst("instructor")?.Value == "true")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "instructor"));

                if (identity.FindFirst("member")?.Value == "true")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "member"));
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
    
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<PilatesDbContext>();
        await context.Database.MigrateAsync();
        Seed.ApplyUsersSeed(context);
        Seed.ApplyClassTypesSeed(context);
        Seed.ApplyScheduledClassesSeed(context);
    }
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RegistrationMiddleware>();
app.MapControllers();
app.Run();

public partial class Program { }