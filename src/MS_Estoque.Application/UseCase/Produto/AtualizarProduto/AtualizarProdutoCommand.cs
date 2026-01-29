using MediatR;

namespace MS_Estoque.Application.UseCase.Produto.AtualizarProduto;

public class AtualizarProdutoCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}