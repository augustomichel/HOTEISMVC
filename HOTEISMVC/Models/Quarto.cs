using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEISMVC.Models
{
    [Table("quartos")]
    public class Quarto
    {
        [Key()]
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        [Column("Id_Hotel")]
        public int Id_Hotel { get; set; }
        public virtual Hotel? Hotel { get; set; }

        [Required]
        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Column("NumOcupantes")]
        [Display(Name = "Número Ocupantes")]
        public int NumOcupantes { get; set; }

        [Required]
        [Column("NumAdultos")]
        [Display(Name = "Número de Adultos")]
        public int NumAdultos { get; set; }

        [Required]
        [Column("NumCriancas")]
        [Display(Name = "Número de Crianças")]
        public int NumCriancas { get; set; }

        [Required]
        [Column("Preco")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

    }
}
