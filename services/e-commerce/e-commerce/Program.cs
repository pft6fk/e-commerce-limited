using e_commerce.Api.Middleware;
using e_commerce.Application;
using e_commerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
