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
                // Primeiro, estamos criando uma lista de reivindicações (claims). 
                // As reivindicações são informações sobre o usuário que queremos incluir no token.
                var claims = new[]
                {
                    // Aqui estamos adicionando o e-mail do usuário como uma reivindicação.
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.email),
                    // Aqui estamos adicionando o ID do usuário como uma reivindicação.
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.idUsuario.ToString()),
                    // Aqui estamos adicionando o papel do usuário como uma reivindicação.
                    new Claim(ClaimTypes.Role, buscaTipoUsuario.titulo)
                };
                                                                                                                                                                                                                                                                            
                // Agora, vamos criar uma chave simétrica. 
                // Esta chave será usada para assinar o token, garantindo que ele não seja alterado após ser emitido.
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Senai-InLock-API-WEB-dasdg34523g"));

                // Com a chave em mãos, vamos criar as credenciais de assinatura. 
                // Estas credenciais incluem a chave e o algoritmo que será usado para assinar o token.
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Finalmente, vamos criar o token. 
                // O token incluirá as reivindicações que criamos, terá um tempo de expiração e será assinado com as credenciais de assinatura.
                var token = new JwtSecurityToken(
                        issuer: "InLock.webApi", // Quem está emitindo o token
                        audience: "InLock.webApi", // Para quem o token é destinado
                        claims: claims, // As reivindicações que incluímos anteriormente
                        expires: DateTime.Now.AddMinutes(30), // O token expirará 30 minutos após ser emitido
                        signingCredentials: creds // As credenciais de assinatura que criamos anteriormente
                        );

                // Por fim, retornamos um objeto anônimo com o token JWT serializado.
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return NotFound();
        }
    }
}
