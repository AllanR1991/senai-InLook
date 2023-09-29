using senai.inLock.webApi.Domains;

namespace senai.inLock.webApi.Interfaces
{
    public interface IJogoRepository
    {
        /// <summary>
        /// Lista todos os Jogo cadastrados.
        /// </summary>
        /// <returns>Lista de Jogo que estão cadastrado no bando de dados</returns>
        public List<JogoDomain> ListaTodos();

        /// <summary>
        /// Busca Jogo por id
        /// </summary>
        /// <param name="id">Id usada para buscar o Jogo</param>
        /// <returns>Retorna um Objeto to tipo JogoDomain</returns>
        public JogoDomain BuscaTipoUsuarioId(int id);

        /// <summary>
        /// Altera o Jogo através da Id passada pela Url
        /// </summary>
        /// <param name="id">Id necessaria para identificação do Jogo.</param>
        /// <param name="jogoAlterado">Objeto to tipo JogoDomain contendo as novas informações do Jogo para alteração</param>
        public void AlterarIdUrl(int id, JogoDomain jogoAlterado);

        /// <summary>
        /// Deletar um Jogo cadastrtado no sistema.
        /// </summary>
        /// <param name="id">Id necessaria para identificar o Jogo a ser deletado.</param>
        public void Deletar(int id);

        /// <summary>
        /// Cadastra um novo Jogo no sistema.
        /// </summary>
        /// <param name="novoJogo">Objeto do tipo JogoDomain contendo os dados do novo Jogo.</param>
        public void Cadastrar(JogoDomain novoJogo);

        public JogoDomain BuscaJogoNome(string nomeJogo);
    }
}
