namespace MS_Estoque.Domain.Entities;

public class EstoqueProdutoEntity : IEntity
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public int QuantidadeDisponivel { get; set; }
}