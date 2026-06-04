using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//falta terminar


namespace ControleEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContasReceberController : ControllerBase
    {
        private readonly IContaReceberService _contaReceberService;

        public ContasReceberController(IContaReceberService contaReceberService)
        {
            _contaReceberService = contaReceberService;
        }

        [HttpGet]// Gerente e Caixa
        public async Task<IActionResult> GetAll()
        {
            var contas = await _contaReceberService.ObterTodosAsync();
            return Ok(contas);
        }

        [HttpGet("{id}")] // Gerente e Caixa, e o cliente busca a própria conta
        public async Task<IActionResult> GetById(int id)
        {
            //buscar do token/claim, cliente só a própria conta, gerente e caixa podem buscar qualquer conta, cliente só a própria conta, se for cliente e o id da conta for diferente do id do cliente, retornar 403 forbiden, se for gerente ou caixa pode buscar qualquer conta.

            if (User.IsInRole("Cliente"))
            {
                var clienteIdClaim = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                var contaCliente = await _contaReceberService.ObterPorIdAsync(id);
                if (contaCliente == null) return NotFound();
                if (contaCliente.ClienteId != clienteIdClaim) return Forbid();
                return Ok(contaCliente);
            }

            var conta = await _contaReceberService.ObterPorIdAsync(id);
            if (conta == null) return NotFound();
            return Ok(conta);
        }

        [HttpPost]// so gerente
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Create([FromBody] CriarContaReceberDto dto)
        {
            var novaConta = await _contaReceberService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = novaConta.Id }, novaConta);
        }

        [HttpPut("{id}")]// só gerente
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarContaReceberDto dto)
        {
            if (id != dto.Id) return BadRequest("O ID da rota difere do ID da conta a receber.");
            
            await _contaReceberService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]  // só gerente
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contaReceberService.RemoverAsync(id);
            return NoContent();
        }
    }
}