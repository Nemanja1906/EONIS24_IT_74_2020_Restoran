using Restoran.Entities;

namespace Restoran.Data
{
    public interface IMusterijaRepository
    {
        
        List<Musterija> GetMusterija();
        Musterija GetMusterijaById(Guid MusterijaID);
        Musterija CreateMusterija(Musterija musterija);
        Musterija UpdateMusterija(Musterija musterija);
        void DeleteMusterija(Guid MusterijaID);
        bool SaveChanges();


    }
}
