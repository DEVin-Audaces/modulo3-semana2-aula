using ExemploTokenBased.Data;
using ExemploTokenBased.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers().Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = "DevInAudaces",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetValue<string>("JWT_SECRET")
                )
            )
        };
    }).Services
    .AddAuthorization(opcoes =>
    {
        opcoes.AddPolicy(
            "SetorVendas",
            config => config
                .RequireClaim(TokenService.ClaimSetorUsuario, "Vendas")
        );
    });

builder.Services
    .AddScoped<IUsuarioService, UsuarioService>()
    .AddScoped<ITokenService, TokenService>()
    .AddScoped<IPasswordHasher, Argon2PasswordHasher>();

builder.Services
    .AddDbContext<TokenBasedContext>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(swagger =>
    {
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Utilizando Token based auth"
        });
        var openApiSecuryRequeriment = new OpenApiSecurityRequirement
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
        };

        swagger.AddSecurityRequirement(openApiSecuryRequeriment);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();