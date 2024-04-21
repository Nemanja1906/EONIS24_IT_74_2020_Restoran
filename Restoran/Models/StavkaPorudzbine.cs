using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Entities
{
    public class StavkaPorudzbine
    {

        public Guid StavkaPorudzbineID { get; set; }
        [ForeignKey("Porudzbina")]
        public Guid PorudzbinaID { get; set; }
        [ForeignKey("Proizvod")]
        public Guid ProizvodID { get; set; }
        public int? Kolicina { get; set; }
    }
}
