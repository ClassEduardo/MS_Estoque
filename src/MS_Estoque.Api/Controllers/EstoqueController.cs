using MediatR;
using Microsoft.AspNetCore.Mvc;
using MS_Estoque.Application.UseCase.Estoque.CriacaoProduto;

namespace MS_Estoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoqueController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public IActionResult PedidoGet()
    {
        return Ok("Pedido");
    }

    [HttpPost]
    public async Task<IActionResult> PedidoPost(CriacaoProdutoCommand command)
    {
       var retorno = await mediator.Send(command);
       return Ok(retorno);
    }
}