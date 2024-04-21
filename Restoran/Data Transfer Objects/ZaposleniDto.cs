using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Dto
{
    public class ZaposleniDto
    {
        [Key]
        public Guid ZaposleniID { get; set; }
        public string? ImeZaposlenog { get; set; }
        public string? PrezimeZaposlenog { get; set; }
        public string? AdresaZaposlenog { get; set; }
        public string? GradZaposlenog { get; set; }
        public string? KontaktZaposlenog { get; set; }
    }
}
