using SuperApp.AccesoDatos;
using SuperApp.AccesoDatos.Interfaz;
using SuperApp.Services.DTOs;
using SuperApp.Services.Sevices;
using SuperApp.Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddTransient((uof) =>
{
    return new UOF();
});
builder.Services.AddScoped<EspecialidadServices>();
builder.Services.AddScoped<UsuarioServices>();
builder.Services.AddScoped<PartidaServices>();
builder.Services.AddScoped<PaginacionDTO>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(["CantidadTotalRegistro"]);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
