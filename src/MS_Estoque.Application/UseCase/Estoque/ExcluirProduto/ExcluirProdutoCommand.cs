using MediatR;

namespace MS_Estoque.Application.UseCase.Estoque.ExcluirProduto;

public class ExcluirProdutoCommand : IRequest<string>
{
    public Guid Id { get; set; }
}