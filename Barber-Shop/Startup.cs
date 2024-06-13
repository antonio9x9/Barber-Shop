using Barber_Shop.ApplicationDbContext;
using Barber_Shop.Models;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Principal;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Barber-Shop", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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
                         new string[] {}
                    }
                });
});

//Estou configurando o banco no AppDbContext no override do onlunched
//string? sqlServerConnection = builder.Configuration.GetConnectionString("SQLServerConnection");
//builder.Services.AddDbContext<AppDbContex>();

//alwayes before build()
builder.Services.AddAuthentication(
                                    JwtBearerDefaults.AuthenticationScheme)
                                    .AddJwtBearer(options =>
                                                  options.TokenValidationParameters = new()
                                                  {
                                                      ValidateIssuer = true,
                                                      ValidateAudience = true,
                                                      LogValidationExceptions = true,
                                                      ValidateLifetime = true,
                                                      ValidAudience = builder.Configuration["TokenConfiguration: Audience"],
                                                      ValidIssuer = builder.Configuration["TokenConfiguration: Issuer"],
                                                      ValidateIssuerSigningKey = true,
                                                      IssuerSigningKey = new SymmetricSecurityKey
                                                      (
                                                          Encoding.UTF8.GetBytes(builder.Configuration["Jwt: Key"])
                                                      ),
                                                  });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContex>()
               .AddDefaultTokenProviders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
