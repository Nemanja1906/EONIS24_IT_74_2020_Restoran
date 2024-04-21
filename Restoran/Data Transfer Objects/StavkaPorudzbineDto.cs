using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Dto
{
    public class StavkaPorudzbineDto
    {
        public Guid StavkaPorudzbineID { get; set; }
        [ForeignKey("Porudzbina")]
        public Guid PorudzbinaID { get; set; }
        [ForeignKey("Proizvod")]
        public Guid ProizvodID { get; set; }
        public int? Kolicina { get; set; }
    }
}
