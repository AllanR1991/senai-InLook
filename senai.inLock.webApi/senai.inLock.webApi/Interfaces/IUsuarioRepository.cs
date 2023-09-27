using senai.inLock.webApi.Domains;
using System.Runtime.CompilerServices;

namespace senai.inLock.webApi.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Lista todos os Usuarios cadastrados.
        /// </summary>
        /// <returns>Lista de usuarios que estão cadastrado no bando de dados</returns>
        public List<UsuarioDomain> ListaTodos();

        /// <summary>
        /// Busca usuario por id
        /// </summary>
        /// <param name="id">Id usada para buscar o usuario</param>
        /// <returns>Retorna um Objeto to tipo UsuarioDomain</returns>
        public UsuarioDomain BuscaUsuarioId(int id);

        /// <summary>
        /// Altera o Usuario através da Id passada pela Url
        /// </summary>
        /// <param name="id">Id necessaria para identificação do usuario.</param>
        /// <param name="usuarioAlterado">Objeto to tipo UsuarioDomain contendo as novas informações do usuario para alteração</param>
        public void AlterarIdUrl(int id, UsuarioDomain usuarioAlterado);

        /// <summary>
        /// Deletar um usuario cadastrtado no sistema.
        /// </summary>
        /// <param name="id">Id necessaria para identificar o Usuario a ser deletado.</param>
        public void Deletar (int id);

        /// <summary>
        /// Cadastra um novo usuario no sistema.
        /// </summary>
        /// <param name="novoUsuario">Objeto do tipo UsuarioDomain contendo os dados do novo Usuario.</param>
        public void Cadastrar(UsuarioDomain novoUsuario);

        /// <summary>
        /// Busca um usuario que corresponda ao email e senha informados.
        /// </summary>
        /// <param name="email">Email a ser buscado</param>
        /// <param name="senha">Senha a ser buscada</param>
        /// <returns>Retorna um Objeto do tipo UsuarioDomain</returns>
        public UsuarioDomain BuscaEmailSenha(string email, string senha);

        /// <summary>
        /// Busca um usuario que corresponde ao email passado.
        /// </summary>
        /// <param name="email">Email a ser buscado</param>
        /// <returns>retorna um objeto do tipo UsuarioDomain</returns>
        public UsuarioDomain BuscaEmail(string email);


    }
}
