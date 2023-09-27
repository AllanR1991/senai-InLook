using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Interfaces;
using senai.inLock.webApi.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai.inLock.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;
        private ITipoUsuarioRepository _tipoUsuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        /// <summary>
        /// Lista todos os Usuarios.
        /// </summary>
        /// <returns>Lista de Usurios</returns>
        /// <response code="200">Retorna a lista de Usurios</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ListarTodo()
        {
            List<UsuarioDomain> listaUsuario = _usuarioRepository.ListaTodos();
            return Ok(listaUsuario);
        }

        /// <summary>
        /// Busca usuario através do Id passado pela URL
        /// </summary>
        /// <param name="id">Id utilizado para buscar Usuario</param>
        /// <returns>Retorna status code Ok contendo, caso contrario retorna NotFound</returns>
        /// <response code="200">Encontrado Usurio.</response>
        /// <response code="404">Usuario encontrado.</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuscaUsuarioId(int id) 
        {
            UsuarioDomain usuarioExiste = _usuarioRepository.BuscaUsuarioId(id);
            if (usuarioExiste != null) 
            {
                return Ok(usuarioExiste);
            }
            return NotFound("Usuario não encontrado.");
        }

        /// <summary>
        /// Cadastra novo Usuario.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///        "idTipoUsuario": Number,
        ///        "email": "exemplo@exemplo.com.br",
        ///        "senha": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="novoUsuario">Objeto contedo os dados do novo usuario</param>
        /// <returns>StatusCode 201</returns>
        /// <response code="201">Novo item criado</response>
        /// <response code="422">Erro usuario ja existe.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Cadastro(UsuarioDomain novoUsuario)
        {
            UsuarioDomain usuarioJaExiste = _usuarioRepository.BuscaEmail(novoUsuario.email);
            if (usuarioJaExiste == null)
            {
                _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201);
            }
            return StatusCode(422);
        }


        /// <summary>
        /// Deleta um usuario do sistema
        /// </summary>
        /// <param name="id">Id passada como parametro para identificar um usuario a ser deletado.</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item deletado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _usuarioRepository.Deletar(id);
            return StatusCode(204);
        }

        /// <summary>
        /// Atualiza um Usuario ja registrado.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT/id
        ///     {
        ///        "idTipoUsuario": Number,
        ///        "email": "exemplo@exemplo.com.br",
        ///        "senha": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Id Utilizado para encontrar o usuario que sera atualizado</param>
        /// <param name="usuarioAlterado">Objeto do tipo UsuarioDomain</param>
        /// <returns>Stuscode 204 No Content</returns>
        /// <response code="204">Item altereado com sucesso, sem conteudo.</response>
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Atualizar(int id,UsuarioDomain usuarioAlterado)
        {
            _usuarioRepository.AlterarIdUrl(id, usuarioAlterado);
            return NoContent();
        }

        /// <summary>
        /// Efetua login no sistema
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST/Login
        ///     {
        ///        "email": "exemplo@exemplo.com.br",
        ///        "senha": "exemplo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="usuario">Objeto do tipo UsarioDomain contendo informações para o login</param>
        /// <returns>Return token, caso contrario return NotFound</returns>
        /// <response code="200">Efetuado login com sucesso.</response>
        /// <response code="404">Usuario ou Email invalidos!</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login(UsuarioDomain usuario)
        {
            UsuarioDomain usuarioEncontrado = _usuarioRepository.BuscaEmailSenha(usuario.email, usuario.senha);

            TipoUsuarioDomain buscaTipoUsuario;
            if(usuarioEncontrado != null)
            {
                buscaTipoUsuario = _tipoUsuarioRepository.BuscaTipoUsuarioId(usuarioEncontrado.idTipoUsuario);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.idUsuario.ToString()),
                    new Claim(ClaimTypes.Role, buscaTipoUsuario.titulo)
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Senai-InLock-API-WEB-dasdg34523g"));

                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: "InLock.webApi",
                        audience: "InLock.webApi",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                        );

                return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
            return NotFound();
        }
    }
}
