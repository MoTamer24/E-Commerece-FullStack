using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities.Identity;
using System.Text;
using Infrastructure; // Your DbContext namespace

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// --- Add your DbContext ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine("here it comes ");
// --- Add ASP.NET Core Identity ---
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
Console.WriteLine("whaaaat ");
// --- Add JWT Authentication ---
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false; // Set to true in production
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Issuer"],
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });


builder.Services.AddControllers();
// ... Add your other services (IUnitOfWork, repositories, etc.)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- IMPORTANT: These must be in this order ---
app.UseAuthentication(); // First, who is the user?
app.UseAuthorization();  // Then, what are they allowed to do?


app.MapControllers();

app.Run();