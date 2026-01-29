using MediatR;
using Newtonsoft.Json;

namespace MS_Estoque.Application.UseCase.Estoque.VerificarReservarEstoque;

public class VerificarReservarEstoqueCommandHandler : IRequestHandler<VerificarReservarEstoqueCommand, string>

{
    public async Task<string> Handle(VerificarReservarEstoqueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"[EstoqueCommandHandler] Verificando estoque para produto: {request.ProdutoId}, quantidade: {request.Quantidade}");

            var estoqueDisponivel = await VerificarEstoqueNoBanco(request.ProdutoId);

            if (estoqueDisponivel >= request.Quantidade)
            {
                await ReservarEstoque(request.ProdutoId, request.Quantidade);

                return JsonConvert.SerializeObject(new
                {
                    sucesso = true,
                    produtoId = request.ProdutoId,
                    quantidadeReservada = request.Quantidade,
                    pedidoId = request.PedidoId,
                    mensagem = "Estoque reservado com sucesso"
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    sucesso = false,
                    produtoId = request.ProdutoId,
                    estoqueDisponivel = estoqueDisponivel,
                    quantidadeSolicitada = request.Quantidade,
                    pedidoId = request.PedidoId,
                    mensagem = "Estoque insuficiente"
                });
            }
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject(new
            {
                sucesso = false,
                mensagem = $"Erro ao processar: {ex.Message}"
            });
        }
    }

    private async Task<int> VerificarEstoqueNoBanco(int produtoId)
    {
        await Task.Delay(100);
        return produtoId <= 10 ? 100 : 0;
    }

    private async Task ReservarEstoque(int produtoId, int quantidade)
    {
        await Task.Delay(50);
        Console.WriteLine($"[EstoqueCommandHandler] Estoque reservado: Produto {produtoId}, Quantidade {quantidade}");
    }
}