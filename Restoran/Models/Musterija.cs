using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Entities
{
    public class Musterija
    {
        [Key]
        public Guid MusterijaID { get; set; }
        public string? ImeMusterije { get; set; }
        public string? PrezimeMusterije { get; set; }
        public string? AdresaMusterije { get; set; }
        public string? GradMusterije { get; set; }
        public string? KontaktMusterije { get; set; }
    }
}
