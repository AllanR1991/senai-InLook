using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace senai.inLock.webApi.Domains
{
    public class TipoUsuarioDomain
    {

        public int idTipoUsuario { get; set; }

        [DisplayName("Tipo Usuario")]
        [Required(ErrorMessage = "Obrigatorio preenchimento do titulo")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "Deve conter no minimo 3 e no maximo 100 caracteres.")]
        [DataType(DataType.Text)]
        public string titulo { get; set; }
    }
}
