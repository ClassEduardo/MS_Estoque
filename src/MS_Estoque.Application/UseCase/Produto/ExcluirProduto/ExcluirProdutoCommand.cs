using MediatR;

namespace MS_Estoque.Application.UseCase.Produto.ExcluirProduto;

public class ExcluirProdutoCommand : IRequest<string>
{
    public Guid Id { get; set; }
}