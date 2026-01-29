using MediatR;
using MS_Estoque.Application.DTO;
using MS_Estoque.Application.Interface;
using MS_Estoque.Application.UseCase.Estoque.VerificarReservarEstoque;
using Newtonsoft.Json;

namespace MS_Estoque.Application.Service;

public class EstoqueService(
    IMediator mediator
) : IEstoqueService
{
    private readonly IMediator _mediator = mediator;

    public async Task<string> VerificarEstoqueAsync(string mensagem)
    {
        try
        {
            var pedidoRequest = JsonConvert.DeserializeObject<PedidoRequest>(mensagem);

            if (pedidoRequest == null)
            {
                return JsonConvert.SerializeObject(new
                {
                    sucesso = false,
                    mensagem = "Formato inválido"
                });
            }

            var command = new VerificarReservarEstoqueCommand
            {
                ProdutoId = pedidoRequest.ProdutoId,
                Quantidade = pedidoRequest.Quantidade,
                PedidoId = pedidoRequest.PedidoId
            };

            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject(new
            {
                sucesso = false,
                mensagem = $"Erro ao processar mensagem: {ex.Message}"
            });
        }
    }
}
