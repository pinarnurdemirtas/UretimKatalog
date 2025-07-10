using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using UretimKatalog.Application.Services; 
using UretimKatalog.Api.Base;
using UretimKatalog.Api.Middleware;
using UretimKatalog.Application.Mappings;
using UretimKatalog.Application.Services;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Infrastructure.Data;
using UretimKatalog.Infrastructure.UnitOfWork;
using UretimKatalog.Infrastructure.Repositories;
using UretimKatalog.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration
var connectionString = builder.Configuration.GetConnectionString("Default");

// 2. EF Core + MySQL (Pomelo 8.x)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33)))
);

// 3. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// 4. Controllers
builder.Services.AddControllers();

// 5. DI: Repositories, UnitOfWork and Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();


// 6. AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// 7. Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 8. CORS
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
    )
);

var app = builder.Build();

// 9. Exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();
builder.WebHost.UseWebRoot("wwwroot");
app.UseStaticFiles();  // wwwroot altında

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

namespace UretimKatalog.Api
{
    public partial class Program { }
}
