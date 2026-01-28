using MediatR;
using MS_Estoque.Domain.Entities;

namespace MS_Estoque.Application.UseCase.Estoque.CriacaoProduto;

public class CriacaoProdutoCommand : IRequest<string>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
