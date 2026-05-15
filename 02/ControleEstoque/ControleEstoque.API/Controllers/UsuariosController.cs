using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.ListarTodosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost("registrar-cliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] CriarClienteDto dto)
        {
            var novoCliente = await _usuarioService.RegistrarClienteAsync(dto);
            return Ok(novoCliente);
        }

        [HttpPost("registrar-caixa")]
        public async Task<IActionResult> RegistrarCaixa([FromBody] CriarCaixaDto dto)
        {
            var novoCaixa = await _usuarioService.RegistrarCaixaAsync(dto);
            return Ok(novoCaixa);
        }

        [HttpPost("registrar-gerente")]
        public async Task<IActionResult> RegistrarGerente([FromBody] CriarGerenteDto dto)
        {
            var novoGerente = await _usuarioService.RegistrarGerenteAsync(dto);
            return Ok(novoGerente);
        }


        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody]LoginDto dto)
        {
            try
            {
                // Tenta executar um trecho de codigo 'perigoso' que pode lançar uma exceção
                var usuario = await _usuarioService.AutenticarAsync(dto);
                if (usuario == null)return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return null;
                // Captura o erro, seja lá qual for, e guarda em no "EX"
            }
            
        }

    }
}