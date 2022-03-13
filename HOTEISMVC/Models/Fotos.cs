using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEISMVC.Models
{
    [Table("fotos_hoteis")]
    public class Fotos
    {
        [Key()]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        [Column("Id_Hotel")]
        public int Id_Hotel { get; set; }
        public virtual Hotel? Hotel { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string? Nome { get; set; }

        [Column("Img")]
        [Display(Name = "Foto")]
        public byte[]? Img { get; set; }
    }
}
