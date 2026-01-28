namespace MS_Estoque.Application.DTO;

public class PedidoRequest
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public string PedidoId { get; set; } = string.Empty;
}