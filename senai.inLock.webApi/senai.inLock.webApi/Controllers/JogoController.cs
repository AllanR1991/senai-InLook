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
    public class JogoController : ControllerBase
    {
        private IJogoRepository _jogoRepository;

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        /// <summary>
        /// Lista todos os Jogo.
        /// </summary>
        /// <returns>Lista de Jogo</returns>
        /// <response code="200">Retorna a lista de Jogo</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ListarTodo()
        {
            List<JogoDomain> listaJogo = _jogoRepository.ListaTodos();
            return Ok(listaJogo);
        }

        /// <summary>
        /// Busca Jogo através do Id passado pela URL
        /// </summary>
        /// <param name="id">Id utilizado para buscar Jogo</param>
        /// <returns>Retorna status code Ok contendo, caso contrario retorna NotFound</returns>
        /// <response code="200">Encontrado Jogo.</response>
        /// <response code="404">Jogo encontrado.</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuscaUsuarioId(int id)
        {
            JogoDomain jogoExiste = _jogoRepository.BuscaTipoUsuarioId(id);
            if (jogoExiste != null)
            {
                return Ok(jogoExiste);
            }
            return NotFound("Jogo não encontrado.");
        }

        /// <summary>
        /// Cadastra novo Jogo.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///        "idEstudio": 0,
        ///        "nomeJogo": "nomeDoJogo",
        ///        "descricao": "Descricao",
        ///        "dataLancamento": "2023-09-28",
        ///        "valor": 19.89        
        ///     }
        ///     
        /// </remarks>
        /// <param name="novoJogo">Objeto contedo os dados do novo Jogo</param>
        /// <returns>StatusCode 201</returns>
        /// <response code="201">Novo item criado</response>
        /// <response code="422">Erro Jogo ja existe.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Cadastro(JogoDomain novoJogo)
        {
            JogoDomain jogoExiste = _jogoRepository.BuscaJogoNome(novoJogo.nomeJogo);
            if (jogoExiste == null)
            {
                _jogoRepository.Cadastrar(novoJogo);
                return StatusCode(201);
            }
            return StatusCode(422);
        }


        /// <summary>
        /// Deleta um Jogo do sistema
        /// </summary>
        /// <param name="id">Id passada como parametro para identificar um Jogo a ser deletado.</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item deletado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _jogoRepository.Deletar(id);
            return StatusCode(204);
        }

        /// <summary>
        /// Atualiza um Jogo ja registrado.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     {
        ///        "idEstudio": 0,
        ///        "nomeJogo": "nomeDoJogo",
        ///        "descricao": "Descricao",
        ///        "dataLancamento": "2023-09-28",
        ///        "valor": 19.89        
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Id Utilizado para encontrar o Jogo que sera atualizado</param>
        /// <param name="tipoUsuarioAlterado">Objeto do tipo JogoDomain</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item altereado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Atualizar(int id, JogoDomain tipoUsuarioAlterado)
        {
            _jogoRepository.AlterarIdUrl(id, tipoUsuarioAlterado);
            return NoContent();
        }
    }
}
