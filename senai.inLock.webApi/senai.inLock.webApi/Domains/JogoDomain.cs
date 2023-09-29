using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace senai.inLock.webApi.Domains
{
    public class JogoDomain
    {
        
        public int idJogo { get; set; }

        [Required(ErrorMessage = "O id estudio é obrigatorio preencher!")]
        public int idEstudio { get; set; }

        [Required(ErrorMessage = "O nome jogo é obrigatorio preencher!")]
        [StringLength(250,ErrorMessage = "O texto deve conter entre 3 e 250 caracteres.",MinimumLength = 3)]
        public string nomeJogo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatorio preencher!")]
        [StringLength(1500, ErrorMessage = "O texto deve conter entre 3 e 1500 caracteres.", MinimumLength = 3)]
        [DataType(DataType.MultilineText)]
        public string descricao { get; set; }

        [Required(ErrorMessage = "A data de lamentação é obrigatorio preencher!")]
        [DataType(DataType.Date)]
        public DateTime dataLancamento { get; set; }

        [Required(ErrorMessage = "O valor é obrigatorio preencher!")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        //Decimal é recomendado quando usamos para valor.
        public decimal valor { get; set; }
    }
}
