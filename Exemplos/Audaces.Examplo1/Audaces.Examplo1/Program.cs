using Audaces.Examplo1.Configurations;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;

var supportedCultures = new[] { 
    new CultureInfo("en-US"), 
    new CultureInfo("pt-BR") 
};
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
    })
    .AddXmlSerializerFormatters()
    .AddMvcOptions(options =>
    {
        options.OutputFormatters.Add(new CsvOutputFormatter());
    });

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCompression();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    DefaultRequestCulture = new RequestCulture("pt-BR")
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();