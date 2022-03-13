using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEISMVC.Models
{
 
    public class HotelDetail
    {
        public Hotel Hotel { get; set; }

        public Quarto Quartos { get; set; }

        public Fotos Fotos { get; set; }
    }
}
