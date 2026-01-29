namespace MS_Estoque.Domain.Entities;

public class EstoqueProdutoEntity
{
    public Guid ProdutoId { get; set; }
    public int QuantidadeDisponivel { get; set; }
}