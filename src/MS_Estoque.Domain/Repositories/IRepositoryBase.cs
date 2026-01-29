namespace MS_Estoque.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T?> ObterPorIdAsync(Guid id);
    Task<T> CriarAsync(T entity);
    Task<T> AtualizarAsync(T entity);
    Task<bool> ExcluirAsync(Guid id);
}