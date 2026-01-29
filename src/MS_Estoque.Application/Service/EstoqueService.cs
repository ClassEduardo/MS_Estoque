using MS_Estoque.Application.DTO;
using MS_Estoque.Application.Interface;
using Newtonsoft.Json;

namespace MS_Estoque.Application.Service;

public class EstoqueService : IEstoqueService
{
    public async Task<string> VerificarEstoqueAsync(string mensagem)
    {
        try
        {
            // Parse da mensagem recebida
            var pedido = JsonConvert.DeserializeObject<PedidoRequest>(mensagem);

            if (pedido == null)
            {
                return JsonConvert.SerializeObject(new
                {
                    sucesso = false,
                    mensagem = "Formato inválido"
                });
            }

            Console.WriteLine($"[EstoqueService] Verificando estoque para produto: {pedido.ProdutoId}, quantidade: {pedido.Quantidade}");

            // Simula verificação de estoque (substitua pela lógica real)
            var estoqueDisponivel = await VerificarEstoqueNoBanco(pedido.ProdutoId);

            if (estoqueDisponivel >= pedido.Quantidade)
            {
                // Reserva o estoque
                await ReservarEstoque(pedido.ProdutoId, pedido.Quantidade);

                return JsonConvert.SerializeObject(new
                {
                    sucesso = true,
                    produtoId = pedido.ProdutoId,
                    quantidadeReservada = pedido.Quantidade,
                    mensagem = "Estoque reservado com sucesso"
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    sucesso = false,
                    produtoId = pedido.ProdutoId,
                    estoqueDisponivel = estoqueDisponivel,
                    quantidadeSolicitada = pedido.Quantidade,
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
        // Simula consulta ao banco
        await Task.Delay(100);

        // Mock: produtos 1-10 têm estoque de 100 unidades
        return produtoId <= 10 ? 100 : 0;
    }

    private async Task ReservarEstoque(int produtoId, int quantidade)
    {
        // Simula atualização no banco
        await Task.Delay(50);
        Console.WriteLine($"[EstoqueService] Estoque reservado: Produto {produtoId}, Quantidade {quantidade}");
    }
}
