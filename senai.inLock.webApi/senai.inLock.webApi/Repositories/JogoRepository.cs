using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Infra;
using senai.inLock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inLock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        public void AlterarIdUrl(int id, JogoDomain jogoAlterado)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdate = @"UPDATE Jogo 
                                            SET IdEstudio = @idEstudio, NomeJogo = @nomeJogo, Descricao = @descricao, DataLancamento = @dataLancamento, Valor = @valor
                                                WHERE IdJogo = @idJogo;";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("IdJogo", id);
                    cmd.Parameters.AddWithValue("idEstudio", jogoAlterado.idEstudio);
                    cmd.Parameters.AddWithValue("nomeJogo", jogoAlterado.nomeJogo);
                    cmd.Parameters.AddWithValue("descricao", jogoAlterado.descricao);
                    cmd.Parameters.AddWithValue("dataLancamento", jogoAlterado.dataLancamento);
                    cmd.Parameters.AddWithValue("valor", jogoAlterado.valor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscaTipoUsuarioId(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectId = "SELECT * FROM Jogo WHERE IdJogo = @idJogo;";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectId, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idJogo", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogoEncontrado = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeJogo = rdr["NomeJogo"].ToString(),
                            descricao = rdr["Descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            valor = Convert.ToDecimal(rdr["Valor"])
                    };
                        return jogoEncontrado;
                    }
                    return null;
                }
            }
        }

        public JogoDomain BuscaJogoNome(string nomeJogo)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectTitulo = "SELECT * FROM Jogo WHERE NomeJogo = @nomeJogo";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectTitulo, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("nomeJogo", nomeJogo);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogoEncontrado = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeJogo = rdr["NomeJogo"].ToString(),
                            descricao = rdr["Descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            valor = Convert.ToDecimal(rdr["Valor"])
                        };
                        return jogoEncontrado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryInsert = "INSERT INTO Jogo Values (@idEstudio, @nomeJogo, @descricao, @dataLancamento, @valor)";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idEstudio", novoJogo.idEstudio);
                    cmd.Parameters.AddWithValue("nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("valor", novoJogo.valor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelete = "DELETE FROM Jogo WHERE IdJogo = @idJogo";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idJogo", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListaTodos()
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectAll = "SELECT * FROM Jogo";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, sqlConexao))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    List<JogoDomain> listaJogos = new List<JogoDomain>();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(rdr["IdJogo"]),
                            idEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            nomeJogo = rdr["NomeJogo"].ToString(),
                            descricao = rdr["Descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            valor = Convert.ToDecimal(rdr["Valor"])
                        };
                        listaJogos.Add(jogo);

                    }
                    return listaJogos;
                }
            };
        }
    }
}
