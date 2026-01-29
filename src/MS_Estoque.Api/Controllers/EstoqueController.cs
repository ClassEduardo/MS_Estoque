using MediatR;
using Microsoft.AspNetCore.Mvc;
using MS_Estoque.Application.UseCase.Estoque.AtualizarProduto;
using MS_Estoque.Application.UseCase.Estoque.ConsultarProduto;
using MS_Estoque.Application.UseCase.Estoque.CriacaoProduto;
using MS_Estoque.Application.UseCase.Estoque.ExcluirProduto;

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

    /// <summary>
    /// Consulta um produto específico por ID
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Dados do produto</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConsultarProduto(Guid id)
    {
        var query = new ConsultarProdutoQuery { Id = id };
        var resultado = await _mediator.Send(query);
        return Ok(resultado);
    }

    /// <summary>
    /// Cria um novo produto
    /// </summary>
    /// <param name="command">Dados do produto a ser criado</param>
    /// <returns>Produto criado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PedidoPost([FromBody] CriacaoProdutoCommand command)
    {
        var resultado = await mediator.Send(command);
        return CreatedAtAction(nameof(ConsultarProduto), new { id = Guid.NewGuid() }, resultado);
    }

    /// <summary>
    /// Atualiza um produto existente
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <param name="command">Dados atualizados do produto</param>
    /// <returns>Produto atualizado</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarProduto(Guid id, [FromBody] AtualizarProdutoCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { mensagem = "ID da rota não corresponde ao ID do corpo da requisição" });
        }

        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }

    /// <summary>
    /// Exclui um produto
    /// </summary>
    /// <param name="id">ID do produto a ser excluído</param>
    /// <returns>Confirmação de exclusão</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExcluirProduto(Guid id)
    {
        var command = new ExcluirProdutoCommand { Id = id };
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }
}