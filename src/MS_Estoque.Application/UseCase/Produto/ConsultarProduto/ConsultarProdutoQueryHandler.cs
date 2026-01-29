using MediatR;
using MS_Estoque.Domain.Entities;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Produto.ConsultarProduto;

public class ConsultarProdutoQueryHandler : IRequestHandler<ConsultarProdutoQuery, string>
{
    public Task<string> Handle(ConsultarProdutoQuery request, CancellationToken cancellationToken)
    {
        // Mock de produto encontrado
        var produto = new ProdutoEntity
        {
            Id = request.Id,
            Nome = "Raquete de Tênis",
            Descricao = "Raquete profissional de alta performance",
            Preco = 450.00m
        };

        return Task.FromResult(JsonConvert.SerializeObject(new
        {
            sucesso = true,
            produto = produto
        }));
    }
}