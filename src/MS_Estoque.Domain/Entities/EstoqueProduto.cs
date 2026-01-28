namespace MS_Estoque.Domain.Entities;

public class EstoqueProduto
{
    public Guid ProdutoId { get; set; }
    public int QuantidadeDisponivel { get; set; }
}