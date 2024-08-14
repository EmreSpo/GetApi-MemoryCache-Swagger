using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekleyin
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

// Swagger'ı ekleyin ve XML yorumlarını dahil edin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AlbilApi",
        Version = "v1",
        Description = "Kullanıcı API'si, JSONPlaceholder'dan kullanıcı bilgilerini alır.",
    });

    // XML yorumlarını dahil edin
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Hata ayıklama sayfasını ve Swagger'ı yalnızca geliştirme ortamında kullanıma alın
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlbilApi v1");
        c.RoutePrefix = string.Empty; // Swagger UI'nin kök URL'de çalışmasını sağlar
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Root URL yönlendirmesi
app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/swagger"); // Kullanıcıyı Swagger UI'ye yönlendirin
    return Task.CompletedTask;
});

app.MapControllers();

app.Run("https://localhost:7061/");
