using OpenDeepSpace.Modularity.Demo.Modules;
using OpenDeepSpace.Modularity.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//������Module
builder.Services.AddModule<ModuleA>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//ʹ��Module
app.UseModule();

app.UseAuthorization();

app.MapControllers();

app.Run();
