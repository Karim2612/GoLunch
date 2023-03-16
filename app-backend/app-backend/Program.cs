using app_backend.Datas;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Identity;
using app_backend.Helpers;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//Flo ajout de SQL Lite + TopolgySuite
string sqlitePath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
//Changement du path de la Db pour les déploiement sur serveur
string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string dbFileName = "Golunch.db";
string absolutePath = Path.Combine(currentPath, dbFileName);
var connectionString = builder.Configuration.GetConnectionString("jesaispas") ?? $"Data Source={absolutePath}";
builder.Services.AddSqlite<GolunchDbContext>(connectionString, x => x.UseNetTopologySuite());
//builder.Services.AddSqlite<GolunchDbContext>(connectionString);

//Flo Pour MySQL
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<GolunchDbContext>(options =>
//{
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.UseNetTopologySuite());
//});




// Flo AddNewtonsoftJson pour les requetes PATCH casse tous les points de Geometries de Netopology...
//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, JsonPatchInputFormat.GetJsonPatchInputFormatter());
});
builder.Services.AddEndpointsApiExplorer();

//Map des models entre eux (register et user)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSwaggerGen(options => {
    //Defini infos sur l'API
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "GoLunch API",
        Description = "An ASP.NET Core Web API for managing Lunch Menus"
    });
    //Ajout du module d'Authorizartion dans Swagger
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    //Flo corrige les requetes PATCH 
    options.DocumentFilter<JsonPatchDocumentFilter>();

    // Chemin pour les commenatires de documentation Swagger JSON & UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

/// <summary>
/// Ajout de l'authentification JWT Bearer Oauth 2.0
/// </summary>
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = configuration["JWT:ValidIssuer"],
            ValidAudience = configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

var app = builder.Build();

//Flo > Ajoute des donn�es par d�fault dans la BDD via Datas/DbInitializer.cs
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GolunchDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI( c => c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "GoLunchAPI"));
}


if (!app.Environment.IsDevelopment())
{
    //Flo plus utile car dans notre cas c'est NGINX qui gère les proxy https
    //app.UseHttpsRedirection();

}

app.UseAuthentication();

app.UseAuthorization();

// Reza : global error handler
// Flo > Reza, bonne initiative mais limite le verbose pour le debug, on désactive pour le moment
//app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
