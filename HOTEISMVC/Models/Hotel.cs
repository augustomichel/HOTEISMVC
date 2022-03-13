using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEISMVC.Models
{
    [Table("hoteis")]
    public class Hotel
    {
        [Key()]
        [Column("Id")]
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Required]
        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Column("CNPJ")]
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [Required]
        [Column("Endereco")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [Column("Descricao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public List<Quarto>? Quartos { get; set; }
        public List<Fotos>? Fotos { get; set; }
    }
}
