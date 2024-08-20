using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore; // Entity Framework Core için ekle
using YourNamespace.Services; // UserService'in bulunduğu namespace'i ekle
using YourNamespace.Data; // AppDbContext'in bulunduğu namespace'i ekle

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı için DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

// UserService ve IUserService'i DI (Dependency Injection) container'a ekle
builder.Services.AddScoped<IUserService, UserService>();

// Swagger'ı ekleyin ve XML yorumlarını dahil et
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

// Hata ayıklama sayfasını ve Swagger'ı yalnızca geliştirme ortamında kullanımı
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
    context.Response.Redirect("/swagger"); // Kullanıcıyı Swagger UI'ye yönlendir
    return Task.CompletedTask;
});

app.MapControllers();

app.Run("https://localhost:7061/");

//https://localhost:7061/api/users
