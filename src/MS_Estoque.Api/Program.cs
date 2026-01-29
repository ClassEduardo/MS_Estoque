

using Microsoft.Extensions.Options;
using MS_Estoque.Application.Interface;
using MS_Estoque.Application.Service;
using MS_Estoque.Domain.Models;
using MS_Estoque.Domain.Repositories;
using MS_Estoque.Infrastructure.Mensageria.Configuracao;
using MS_Estoque.Infrastructure.Mensageria.Interfaces;
using MS_Estoque.Infrastructure.Mensageria.Services;
using MS_Estoque.Infrastructure.Repositories;
using RabbitMQ.Client;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<RabbitMqConfiguracao>(
    builder.Configuration.GetSection("RabbitMQ")
);

builder.Services.AddSingleton<IConnection>(provider =>
{
    var config = provider.GetRequiredService<IOptions<RabbitMqConfiguracao>>().Value;

    var factory = new ConnectionFactory
    {
        HostName = config.HostName,
        Port = config.Port,
        UserName = config.UserName,
        Password = config.Password
    };

    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});

builder.Services.AddMediatR(cfg =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
    foreach (var assembly in assemblies)
    {
        cfg.RegisterServicesFromAssembly(assembly);
    }
});

// Registra o Consumer
builder.Services.AddSingleton<IRabbitMqRpcConsumer, RabbitMqRpcConsumer>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();

builder.Services.AddScoped<IRepositoryBase<ProdutoEntity>>(provider =>
    new JsonRepositoryBase<ProdutoEntity>("produtos.json"));

builder.Services.AddScoped<IRepositoryBase<NotaFiscalEntity>>(provider =>
    new JsonRepositoryBase<NotaFiscalEntity>("notas-fiscais.json"));

builder.Services.AddScoped<IRepositoryBase<EstoqueProdutoEntity>>(provider =>
    new JsonRepositoryBase<EstoqueProdutoEntity>("estoques-produtos.json"));

var app = builder.Build();

// Diz pra qual service devem ir as mesagens de vindas de qual fila
// poderia ser feito de forma mais escalável
var consumer = app.Services.GetRequiredService<IRabbitMqRpcConsumer>();
var estoqueService = app.Services.CreateScope().ServiceProvider.GetRequiredService<IEstoqueService>();
_ = consumer.StartListeningAsync("task_queue", estoqueService.VerificarEstoqueAsync);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();