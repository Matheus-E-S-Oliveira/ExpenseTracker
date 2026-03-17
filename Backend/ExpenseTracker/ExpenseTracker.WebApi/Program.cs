using ExpenseTracker.WebApi.Structure.Endpoints.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//builder.Services.Scan(scan => scan
//    .FromApplicationDependencies()
//    .AddClasses(c => c.AssignableTo(typeof(IEndpointHandler<>)))
//    .AsImplementedInterfaces()
//    .WithScopedLifetime());

//builder.Services.AddValidatorsFromAssemblyContaining<Program>();

#region MediatR

/// <summary>
/// Registra o MediatR no container de injeção de dependências.
/// O MediatR implementa o padrão Mediator, permitindo desacoplar
/// a comunicação entre os Controllers e os handlers responsáveis
/// por processar as requisições.
/// </summary>
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

#endregion

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
