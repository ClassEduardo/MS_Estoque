using MediatR;

namespace MS_Estoque.Application.UseCase.Produto.ConsultarProduto;

public class ConsultarProdutoQuery : IRequest<string>
{
    public Guid Id { get; set; }
}