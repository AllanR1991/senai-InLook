using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Interfaces;
using senai.inLock.webApi.Repositories;

namespace senai.inLock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        /// <summary>
        /// Lista todos os TipoUsuario.
        /// </summary>
        /// <returns>Lista de TipoUsuario</returns>
        /// <response code="200">Retorna a lista de TipoUsuario</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ListarTodo()
        {
            List<TipoUsuarioDomain> listaTipoUsuario = _tipoUsuarioRepository.ListaTodos();
            return Ok(listaTipoUsuario);
        }

        /// <summary>
        /// Busca TipoUsuario através do Id passado pela URL
        /// </summary>
        /// <param name="id">Id utilizado para buscar TipoUsuario</param>
        /// <returns>Retorna status code Ok contendo, caso contrario retorna NotFound</returns>
        /// <response code="200">Encontrado TipoUsuario.</response>
        /// <response code="404">TipoUsuario encontrado.</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuscaUsuarioId(int id)
        {
            TipoUsuarioDomain tipoUsuarioExiste = _tipoUsuarioRepository.BuscaTipoUsuarioId(id);
            if (tipoUsuarioExiste != null)
            {
                return Ok(tipoUsuarioExiste);
            }
            return NotFound("TipoUsuario não encontrado.");
        }

        /// <summary>
        /// Cadastra novo TipoUsuario.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///        "titulo": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="novoTipoUsuario">Objeto contedo os dados do novo TipoUsuario</param>
        /// <returns>StatusCode 201</returns>
        /// <response code="201">Novo item criado</response>
        /// <response code="422">Erro TipoUsuario ja existe.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Cadastro(TipoUsuarioDomain novoTipoUsuario)
        {
            TipoUsuarioDomain tipoUsuarioExiste = _tipoUsuarioRepository.BuscaTipoUsuarioTitulo(novoTipoUsuario.titulo);
            if(tipoUsuarioExiste == null)
            {
                _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
                return StatusCode(201);
            }
            return StatusCode(422);
        }


        /// <summary>
        /// Deleta um TipoUsuario do sistema
        /// </summary>
        /// <param name="id">Id passada como parametro para identificar um TipoUsuario a ser deletado.</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item deletado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _tipoUsuarioRepository.Deletar(id);
            return StatusCode(204);
        }

        /// <summary>
        /// Atualiza um TipoUsuario ja registrado.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT/id
        ///     {
        ///        "titulo": "exemplo",     
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Id Utilizado para encontrar o TipoUsuario que sera atualizado</param>
        /// <param name="tipoUsuarioAlterado">Objeto do tipo TipoUsuarioDomain</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item altereado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Atualizar(int id, TipoUsuarioDomain tipoUsuarioAlterado)
        {
            _tipoUsuarioRepository.AlterarIdUrl(id, tipoUsuarioAlterado);
            return NoContent();
        }
    }
}
