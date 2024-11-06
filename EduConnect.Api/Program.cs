using EduConnect.Api.Middlewares;
using EduConnect.Api.Validations;
using EduConnect.Core.Configuration;
using EduConnect.Core.Identity;
using EduConnect.Core.Models;
using EduConnect.Core.Repositories;
using EduConnect.Core.UnitOfWorks;
using EduConnect.Data.Configurations;
using EduConnect.Data.Context;
using EduConnect.Repository.Repositories;
using EduConnect.Repository.UnitOfWork;
using EduConnect.Services.Abstract;
using EduConnect.Services.Concrete;
using EduConnect.Services.ConcreteTokenService;
using EduConnect.Services.Extensions;
using EduConnect.Services.Mapping;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IAuthenticationService = EduConnect.Services.Abstract.IAuthenticationService;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CourseDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<StudentDtoValidator>();




builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//Dependency Injection 
builder.Services.AddScoped<IAuthenticationService, AuthenticationManager>();
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<ICourseService, CourseManager>();
builder.Services.AddScoped<ICourseRepository,CourseRepository>();
builder.Services.AddScoped<IUserService, UserManagerService>();
builder.Services.AddScoped<ITokenService, TokenManager>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(typeof(MapProfile));


// Appsettings'den TokenOptions'u yükle
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// JWT yapýlandýrmasýný uygula
builder.Services.AddCustomTokenAuth(builder.Configuration);




builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Bearer token 'Bearer {token}' formatýnda giriniz.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();


app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(roleManager);
}

app.Run();
