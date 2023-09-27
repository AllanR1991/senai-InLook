using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Infra;
using senai.inLock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inLock.webApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        public void AlterarIdUrl(int id, TipoUsuarioDomain tipoAlterado)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdate = @"UPDATE TipoUsuario 
                                            SET Titulo = @titulo
                                                WHERE IdTipoUsuario = @idTipoUsuario;";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idTipoUsuario", id);
                    cmd.Parameters.AddWithValue("titulo", tipoAlterado.titulo);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TipoUsuarioDomain BuscaTipoUsuarioId(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectId = "SELECT * FROM TipoUsuario WHERE IdTipoUsuario = @idTipoUsuario;";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectId, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idTipoUsuario", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TipoUsuarioDomain usuarioEncontrado = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            titulo = rdr["Titulo"].ToString()                            
                        };
                        return usuarioEncontrado;
                    }
                    return null;
                }
            }
        }

        public TipoUsuarioDomain BuscaTipoUsuarioTitulo(string titulo)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectTitulo = "SELECT * FROM TipoUsuario WHERE Titulo = @titulo";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectTitulo, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("titulo", titulo);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        TipoUsuarioDomain tipoUsuarioEncontrado = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            titulo = rdr["Titulo"].ToString()
                        };
                        return tipoUsuarioEncontrado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryInsert = "INSERT INTO TipoUsuario Values (@titulo)";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("titulo", novoTipoUsuario.titulo);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelete = "DELETE FROM TipoUsuario WHERE IdTipoUsuario = @idTipoUsuario";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idTipoUsuario", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TipoUsuarioDomain> ListaTodos()
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectAll = "SELECT * FROM TipoUsuario";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, sqlConexao))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    List<TipoUsuarioDomain> listaUsuarios = new List<TipoUsuarioDomain>();

                    while (rdr.Read())
                    {
                        TipoUsuarioDomain usuario = new TipoUsuarioDomain()
                        {
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            titulo = rdr["Titulo"].ToString()                            
                        };
                        listaUsuarios.Add(usuario);

                    }
                    return listaUsuarios;
                }
            };
        }
    }
}
