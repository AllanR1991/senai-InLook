using senai.inLock.webApi.Domains;

namespace senai.inLock.webApi.Interfaces
{
    public interface IEstudioRepository
    {
        /// <summary>
        /// Lista todos os Estudio cadastrados.
        /// </summary>
        /// <returns>Lista de Estudio que estão cadastrado no bando de dados</returns>
        public List<EstudioDomain> ListaTodos();

        /// <summary>
        /// Busca estudio por id
        /// </summary>
        /// <param name="id">Id usada para buscar o estudio</param>
        /// <returns>Retorna um Objeto to tipo EstudioDomain</returns>
        public EstudioDomain BuscaEstudioId(int id);

        /// <summary>
        /// Altera o estudio através da Id passada pela Url
        /// </summary>
        /// <param name="id">Id necessaria para identificação do estudio.</param>
        /// <param name="estudioAlterado">Objeto do tipo EstudioDomain contendo as novas informações do estudio para alteração</param>
        public void AlterarIdUrl(int id, EstudioDomain estudioAlterado);

        /// <summary>
        /// Deletar um estudio cadastrtado no sistema.
        /// </summary>
        /// <param name="id">Id necessaria para identificar o estudio a ser deletado.</param>
        public void Deletar(int id);

        /// <summary>
        /// Cadastra um novo estudio no sistema.
        /// </summary>
        /// <param name="novoEstudio">Objeto do tipo EstudioDomain contendo os dados do novo estudio.</param>
        public void Cadastrar(EstudioDomain novoEstudio);

        /// <summary>
        /// Busca um estudio que corresponde ao nomeEstudio passado.
        /// </summary>
        /// <param name="nomeEstudio">nomeEstudio a ser buscado</param>
        /// <returns>retorna um objeto do tipo EstudioDomain</returns>
        public EstudioDomain BuscaNomeEstudio(string nomeEstudio);
    }
}
