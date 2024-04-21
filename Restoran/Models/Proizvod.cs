using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Entities
{
    public class Proizvod
    {
       
        [Key]
        public Guid ProizvodID { get; set; }
        public string? Hrana { get; set; }
        public string? Pice { get; set; }
        public string? NazivProizvoda { get; set; }
        public double? Cena { get; set; }
        public string? Opis { get; set; }
    }
}
