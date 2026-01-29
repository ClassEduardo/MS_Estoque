using MediatR;
using MS_Estoque.Domain.Entities;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Estoque.AtualizarProduto;

public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, string>
{
    public Task<string> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto
        {
            Id = request.Id,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco
        };

        return Task.FromResult(JsonConvert.SerializeObject(new
        {
            sucesso = true,
            mensagem = "Produto atualizado com sucesso",
            produto = produto
        }));
    }
}