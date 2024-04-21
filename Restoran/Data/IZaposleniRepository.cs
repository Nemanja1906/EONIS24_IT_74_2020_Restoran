using Restoran.Entities;

namespace Restoran.Data
{
    public interface IZaposleniRepository
    {
        
        List<Zaposleni> GetZaposleni();
        Zaposleni GetZaposleniById(Guid ZaposleniID);
        Zaposleni CreateZaposleni(Zaposleni zaposleni);
        Zaposleni UpdateZaposleni(Zaposleni zaposleni);
        void DeleteZaposleni(Guid ZaposleniID);
        bool SaveChanges();


    }
}
