using MediatR;
using MS_Estoque.Domain.Models;
using MS_Estoque.Domain.Repositories;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Produto.CriacaoProduto;

public class CriacaoProdutoCommandHandler(IRepositoryBase<ProdutoEntity> produtoRepository) : IRequestHandler<CriacaoProdutoCommand, string>
{
    private readonly IRepositoryBase<ProdutoEntity> _produtoRepository = produtoRepository;

    public async Task<string> Handle(CriacaoProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new ProdutoEntity
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco
        };

        var produtoCriado = await _produtoRepository.CriarAsync(produto);

        return JsonConvert.SerializeObject(new
        {
            sucesso = true,
            mensagem = "Produto criado com sucesso",
            produto = produtoCriado
        });
    }
}