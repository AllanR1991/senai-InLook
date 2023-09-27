using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace senai.inLock.webApi.Domains
{
    public class UsuarioDomain
    {
        [DisplayName("Id Usuario")]
        public int idUsuario { get; set; }

        [DisplayName("Tipo Usuario")]//Define o texto que será visível para uma propriedade quando usada em um campo de formulário
        [Required(ErrorMessage = "O campo tipo usuario é obrigatório.",AllowEmptyStrings = false)]
        public int idTipoUsuario { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "O campo email é obrigatório.", AllowEmptyStrings = false)]
        public string email { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo senha é obrigatório.", AllowEmptyStrings = false)]
        public string senha { get; set; }
    }
}
