using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Dto
{
    public class PorudzbinaDto
    {
        [Key]
        public Guid PorudzbinaID { get; set; }
        public double? IznosPorudzbine { get; set; }
        public string? NacinPlacanja { get; set; }
        public string? DodeljeniSto { get; set; }
        public string? StatusPorudzbine { get; set; }

        [ForeignKey("Musterija")]
        public Guid MusterijaID { get; set; }

        [ForeignKey("Zaposleni")]
        public Guid ZaposleniID { get; set; }
    }
}
