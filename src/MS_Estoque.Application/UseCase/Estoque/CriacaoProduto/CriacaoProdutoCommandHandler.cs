using MediatR;
using MS_Estoque.Domain.Entities;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Estoque.CriacaoProduto;

public class CriacaoProdutoCommandHandler : IRequestHandler<CriacaoProdutoCommand, string>
{
    public Task<string> Handle(CriacaoProdutoCommand request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var produto = new Produto
        {
            Id = guid,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco
        };

        return Task.FromResult(JsonConvert.SerializeObject(produto));
    }
}