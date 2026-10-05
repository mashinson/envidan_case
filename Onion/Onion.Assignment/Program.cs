using Onion.Application;
using Onion.Assignment.Extensions;
using Onion.Assignment.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddSwaggerDocumentation();

var app = builder.Build();

app.UseExceptionHandler();
app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();

public partial class Program;
