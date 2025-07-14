using System.Text;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UretimKatalog.Api.Middleware;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Identity.Data;
using UretimKatalog.Identity.Data.Configurations;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Api.Endpoints;
using UretimKatalog.Application.Contracts.Identity;
using UretimKatalog.Application.Features.Auth.Mapping;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using System.Reflection;
using UretimKatalog.Application.Behaviors;
using UretimKatalog.Application.Features.Auth.Handlers.Commands;
using UretimKatalog.Application.Features.Auth.Validators.Commands;
using UretimKatalog.Application.Middleware;
using UretimKatalog.Application.Features.Product.Validators.Commands;
using UretimKatalog.Application.Features.Product.Validators.Queries;
using UretimKatalog.Application.Features.Product.Requests.Queries;
using UretimKatalog.Application.Features.Product.Handlers.Queries;
using UretimKatalog.Application.Features.Product.Handlers.Commands;
using UretimKatalog.Identity.Services;
using UretimKatalog.Persistence.Repositories;
using UretimKatalog.Persistence.UnitOfWork;
using UretimKatalog.Application.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(
    Assembly.GetExecutingAssembly(),
    typeof(ProductCommandHandler).Assembly,
    typeof(ProductQueryHandler).Assembly,
    typeof(AuthCommandHandler).Assembly
);

builder.Services.AddLocalization();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetProductByIdValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 33)))
);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddAutoMapper(typeof(AuthProfile).Assembly);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UretimKatalog API", Version = "v1" });

    var jwtScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtScheme, Array.Empty<string>() }
    });
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();


var app = builder.Build();

builder.WebHost.UseWebRoot("wwwroot");
app.UseStaticFiles();

app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

else
{
    app.UseExceptionHandler(appErr =>
    {
        appErr.Run(async ctx =>
        {
            var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex is ValidationException ve)
            {
                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                await ctx.Response.WriteAsJsonAsync(new
                {
                    Message = "Validation failed",
                    Errors = ve.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
                return;
            }
        });
    });
}



app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuth();
app.MapProducts();
app.MapCategories();
app.MapOrders();
app.MapReviews();

app.Run();

namespace UretimKatalog.Api { public partial class Program { } }
