using MediatR;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Produto.ExcluirProduto;

public class ExcluirProdutoCommandHandler : IRequestHandler<ExcluirProdutoCommand, string>
{
    public Task<string> Handle(ExcluirProdutoCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(JsonConvert.SerializeObject(new
        {
            sucesso = true,
            mensagem = "Produto excluído com sucesso",
            produtoId = request.Id
        }));
    }
}