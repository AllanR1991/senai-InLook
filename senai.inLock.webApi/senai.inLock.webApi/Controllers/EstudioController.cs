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
    public class EstudioController : ControllerBase
    {
        IEstudioRepository _estudioRepository;

        public EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        /// <summary>
        /// Lista todos os Estudios.
        /// </summary>
        /// <returns>Lista de Estudios</returns>
        /// <response code="200">Retorna a lista de Estudios</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ListarTodo()
        {
            List<EstudioDomain> listaUsuario = _estudioRepository.ListaTodos();
            return Ok(listaUsuario);
        }

        /// <summary>
        /// Busca estudio através do Id passado pela URL
        /// </summary>
        /// <param name="id">Id utilizado para buscar estudio</param>
        /// <returns>Retorna status code Ok contendo, caso contrario retorna NotFound</returns>
        /// <response code="200">Encontrado estudio.</response>
        /// <response code="404">estudio não encontrado.</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuscaUsuarioId(int id)
        {
            EstudioDomain estudioExiste = _estudioRepository.BuscaEstudioId(id);
            if (estudioExiste != null)
            {
                return Ok(estudioExiste);
            }
            return NotFound("estudio não encontrado.");
        }

        /// <summary>
        /// Cadastra novo estudio.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///        "nomeEstudio": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="novoEstudio">Objeto contedo os dados do novo estudio</param>
        /// <returns>StatusCode 201</returns>
        /// <response code="201">Novo item criado</response>
        /// <response code="422">Erro estudio ja existe.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Cadastro(EstudioDomain novoEstudio)
        {
            EstudioDomain usuarioJaExiste = _estudioRepository.BuscaNomeEstudio(novoEstudio.nomeEstudio);
            if (usuarioJaExiste == null)
            {
                _estudioRepository.Cadastrar(novoEstudio);
                return StatusCode(201);
            }
            return StatusCode(422);
        }


        /// <summary>
        /// Deleta um estudio do sistema
        /// </summary>
        /// <param name="id">Id passada como parametro para identificar um estudio a ser deletado.</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item deletado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _estudioRepository.Deletar(id);
            return StatusCode(204);
        }

        /// <summary>
        /// Atualiza um estudio ja registrado.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT/id
        ///     {
        ///        "nomeEstudio": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Id Utilizado para encontrar o estudio que sera atualizado</param>
        /// <param name="estudioAlterado">Objeto do tipo EstudioDomain</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item altereado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Atualizar(int id, EstudioDomain estudioAlterado)
        {
            _estudioRepository.AlterarIdUrl(id, estudioAlterado);
            return NoContent();
        }
    }
}
