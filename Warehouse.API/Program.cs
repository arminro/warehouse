using Serilog;
using Warehouse.API.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddWarehouseDepenendencies(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .ReadFrom
    .Configuration(context.Configuration);

    configuration
    .WriteTo
    .MSSqlServer(context.Configuration.GetConnectionString("WarehouseDb"), "Logs", autoCreateSqlTable: true);

    configuration
    .WriteTo
    .Console();
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
