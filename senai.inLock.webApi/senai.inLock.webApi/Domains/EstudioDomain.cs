using System.ComponentModel.DataAnnotations;

namespace senai.inLock.webApi.Domains
{
    public class EstudioDomain
    {
        public int idEstudio { get; set; }

        [Required(ErrorMessage = "O campo nome estudio deve ser preenchido! ")]
        [StringLength(250,MinimumLength =3,ErrorMessage ="O texto deve conter entre 3 e 250 caracteres.")]
        public string nomeEstudio { get; set; }

    }
}
