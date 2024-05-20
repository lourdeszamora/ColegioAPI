using ColegioAPI.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Add services to the container.
builder.Services.AddDbContext<ColegioContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AlumnoRepository>();
builder.Services.AddScoped<GradoRepository>();
builder.Services.AddScoped<ProfesorRepository>();
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions( opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ColegioContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    if (allowedOrigins != null) options.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
