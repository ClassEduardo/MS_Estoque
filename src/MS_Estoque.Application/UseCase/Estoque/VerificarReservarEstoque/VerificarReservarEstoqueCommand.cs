using MediatR;

namespace MS_Estoque.Application.UseCase.Estoque.VerificarReservarEstoque;

public class VerificarReservarEstoqueCommand : IRequest<string>
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public string PedidoId { get; set; } = string.Empty;
}