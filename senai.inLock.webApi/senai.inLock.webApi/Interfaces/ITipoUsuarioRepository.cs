using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Infra;
using System.Data.SqlClient;

namespace senai.inLock.webApi.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        /// <summary>
        /// Lista todos os TipoUsuarios cadastrados.
        /// </summary>
        /// <returns>Lista de TipoUsuarios que estão cadastrado no bando de dados</returns>
        public List<TipoUsuarioDomain> ListaTodos();

        /// <summary>
        /// Busca TipoUsuario por id
        /// </summary>
        /// <param name="id">Id usada para buscar o TipoUsuario</param>
        /// <returns>Retorna um Objeto to tipo TipoUsuarioDomain</returns>
        public TipoUsuarioDomain BuscaTipoUsuarioId(int id);

        /// <summary>
        /// Altera o TipoUsuario através da Id passada pela Url
        /// </summary>
        /// <param name="id">Id necessaria para identificação do TipoUsuario.</param>
        /// <param name="tipoAlterado">Objeto to tipo TipoUsuarioDomain contendo as novas informações do TipoUsuario para alteração</param>
        public void AlterarIdUrl(int id, TipoUsuarioDomain tipoAlterado);

        /// <summary>
        /// Deletar um TipoUsuario cadastrtado no sistema.
        /// </summary>
        /// <param name="id">Id necessaria para identificar o TipoUsuario a ser deletado.</param>
        public void Deletar(int id);

        /// <summary>
        /// Cadastra um novo TipoUsuario no sistema.
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto do tipo TipoUsuarioDomain contendo os dados do novo TipoUsuario.</param>
        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario);

        public TipoUsuarioDomain BuscaTipoUsuarioTitulo(string titulo);
        
    }
}
