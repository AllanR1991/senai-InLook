using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Infra;
using senai.inLock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inLock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        /// <summary>
        /// Altera o estudio sobre o qual foi passado a ID vua URL.
        /// </summary>
        /// <param name="id">ID utilizada para encontrar o estudio.</param>
        /// <param name="estudioAlterado">Objeto do tipo EstudioDomain contendo os dados alterados.</param>
        public void AlterarIdUrl(int id, EstudioDomain estudioAlterado)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdate = @"UPDATE estudio 
                                            SET NomeEstudio = @nomeEstudio
                                                WHERE IdEstudio = @idEstudio;";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idEstudio", id);
                    cmd.Parameters.AddWithValue("nomeEstudio", estudioAlterado.nomeEstudio);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Busca estudio através do Id passado pela URL
        /// </summary>
        /// <param name="id">ID utilizada para procurar um estudio com a mesma Id.</param>
        /// <returns>Se o Id existir retorna um objeto do tipo EstudioDomain chamado estudioEncontrado, caso contrario retorna null.</returns>
        public EstudioDomain BuscaEstudioId(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectId = "SELECT * FROM estudio WHERE IdEstudio = @idEstudio";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectId, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idEstudio", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EstudioDomain estudioEncontrado = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeEstudio = rdr["NomeEstudio"].ToString()
                        };
                        return estudioEncontrado;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Busca um estudio pelo nome
        /// </summary>
        /// <param name="nomeEstudio">uma string utilizada para encontrar o nome do estudio</param>
        /// <returns>um objeto do tipo EstudioDomain</returns>
        public EstudioDomain BuscaNomeEstudio(string nomeEstudio)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectEmailSenha = "SELECT * FROM Estudio WHERE NomeEstudio = @nomeEstudio";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectEmailSenha, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("nomeEstudio", nomeEstudio);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EstudioDomain estudioEncontrado = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeEstudio = rdr["NomeEstudio"].ToString()
                        };
                        return estudioEncontrado;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo estudio.
        /// </summary>
        /// <param name="novoEstudio">Objeto to tipo EstudioDomain contendo dados do novo estudio.</param>
        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryInsert = "INSERT INTO estudio Values (@nomeEstudio)";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("nomeEstudio", novoEstudio.nomeEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deleta o Estudio que foi passado pela url.
        /// </summary>
        /// <param name="id">Id que identifica o estudio a ser deletado.</param>
        public void Deletar(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelete = "DELETE FROM Estudio WHERE IdEstudio = @idEstudio";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idEstudio", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os estudios contido no sistema
        /// </summary>
        /// <returns>Lista de estudio do tipo EstudioDomain</returns>
        public List<EstudioDomain> ListaTodos()
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectAll = "SELECT * FROM Estudio";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, sqlConexao))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    List<EstudioDomain> listaUsuarios = new List<EstudioDomain>();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeEstudio = rdr["NomeEstudio"].ToString()
                        };
                        listaUsuarios.Add(estudio);

                    }
                    return listaUsuarios;
                }
            };
        }
    }
}
