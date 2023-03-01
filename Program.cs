using Microsoft.Extensions.Logging.Console;
using Taxes.Data;
using Taxes.Services;

var builder = WebApplication.CreateBuilder(args);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Disabled);
});

var logger = loggerFactory.CreateLogger<Program>();

builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddScoped<IImportService, ImportService>();

builder.Services.AddDbContext<DBDataContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
