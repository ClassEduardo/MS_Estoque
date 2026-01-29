namespace MS_Estoque.Domain.Entities;

public class NotaFiscalEntity
{
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public string NumeroNotaFiscal { get; set; } = string.Empty;
}