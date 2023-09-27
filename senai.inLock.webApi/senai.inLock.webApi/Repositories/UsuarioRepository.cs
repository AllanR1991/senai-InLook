    using senai.inLock.webApi.Domains;
using senai.inLock.webApi.Infra;
using senai.inLock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inLock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        /// <summary>
        /// Altera o usuario sobre o qual foi passado a ID vua URL.
        /// </summary>
        /// <param name="id">ID utilizada para encontrar o Usuario.</param>
        /// <param name="usuarioAlterado">Objeto do tipo UsuarioDomain contendo os dados alterados.</param>
        public void AlterarIdUrl(int id, UsuarioDomain usuarioAlterado)
        {
            using(SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdate = @"UPDATE Usuario 
                                            SET IdTipoUsuario = @idTipoUsuario, Email = @email, Senha = @senha 
                                                WHERE IdUsuario = @idUsuario;";

                sqlConexao.Open();

                using(SqlCommand cmd = new SqlCommand(queryUpdate, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idUsuario", id);
                    cmd.Parameters.AddWithValue("idTipoUsuario", usuarioAlterado.idTipoUsuario);
                    cmd.Parameters.AddWithValue("email", usuarioAlterado.email);
                    cmd.Parameters.AddWithValue("senha", usuarioAlterado.senha);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscaEmail(string email)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectEmailSenha = "SELECT * FROM Usuario WHERE Email = @email";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectEmailSenha, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("email", email);
              
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            email = rdr["Email"].ToString(),
                            senha = rdr["Senha"].ToString()
                        };
                        return usuarioEncontrado;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Busca um usuario que corresponda ao email e senha passada.
        /// </summary>
        /// <param name="email">Email utilizado para pesquisar usuario</param>
        /// <param name="senha">Senha utilizado para pesquisar usuario</param>
        /// <returns>Se o Id existir retorna um objeto do tipo UsuarioDomain chamado usuarioEncontrado, caso contrario retorna null.</returns>
        public UsuarioDomain BuscaEmailSenha(string email, string senha)
        {
            using(SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectEmailSenha = "SELECT * FROM Usuario WHERE Email = @email AND Senha = @senha";

                sqlConexao.Open();

                using(SqlCommand cmd = new SqlCommand(querySelectEmailSenha, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("senha", senha);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            email = rdr["Email"].ToString(),
                            senha = rdr["Senha"].ToString()
                        };
                        return usuarioEncontrado;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Busca Usuario através do Id passado pela URL
        /// </summary>
        /// <param name="id">ID utilizada para procurar um Usuario com a mesma Id.</param>
        /// <returns>Se o Id existir retorna um objeto do tipo UsuarioDomain chamado usuarioEncontrado, caso contrario retorna null.</returns>
        public UsuarioDomain BuscaUsuarioId(int id)
        {
            using(SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectId = "SELECT * FROM Usuario WHERE IdUsuario = @idUsuario";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectId, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idUsuario", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            email = rdr["Email"].ToString(),
                            senha = rdr["Senha"].ToString()
                        };
                        return usuarioEncontrado;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <param name="novoUsuario">Objeto to tipo UsuarioDomain contendo dados do novo usuario.</param>
        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            
                using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
                {
                    string queryInsert = "INSERT INTO Usuario Values (@idTipoUsuario,@email,@senha)";

                    sqlConexao.Open();

                    using (SqlCommand cmd = new SqlCommand(queryInsert, sqlConexao))
                    {
                        cmd.Parameters.AddWithValue("idTipoUsuario", novoUsuario.idTipoUsuario);
                        cmd.Parameters.AddWithValue("email", novoUsuario.email);
                        cmd.Parameters.AddWithValue("senha", novoUsuario.senha);

                        cmd.ExecuteNonQuery();
                    }
                }
            
        }

        /// <summary>
        /// Deleta o Usurio que foi passado pela url.
        /// </summary>
        /// <param name="id">Id que identifica o usuario a ser deletado.</param>
        public void Deletar(int id)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelete = "DELETE FROM Usuario WHERE IdUsuario = @idUsuario";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryDelete, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idUsuario", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os usuarios contido no sistema
        /// </summary>
        /// <returns>Lista de Usuario do tiipo UsuarioDomain</returns>
        public List<UsuarioDomain> ListaTodos()
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectAll = "SELECT * FROM Usuario";

                sqlConexao.Open();

                using(SqlCommand cmd = new SqlCommand(querySelectAll, sqlConexao))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    List<UsuarioDomain> listaUsuarios = new List<UsuarioDomain>();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            email = rdr["Email"].ToString(),
                            senha = rdr["Senha"].ToString()
                        };
                        listaUsuarios.Add(usuario);
                        
                    }
                    return listaUsuarios;
                }
            };
        }
    }
}
