namespace MS_Estoque.Domain.Models;

public class EstoqueProdutoEntity : IEntity
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public int QuantidadeDisponivel { get; set; }
}