using Restoran.Entities;

namespace Restoran.Data
{
    public interface IProizvodRepository
    {
        
        List<Proizvod> GetProizvod();
        Proizvod GetProizvodById(Guid ProizvodID);
        Proizvod CreateProizvod(Proizvod proizvod);
        Proizvod UpdateProizvod(Proizvod proizvod);
        void DeleteProizvod(Guid ProizvodID);
        bool SaveChanges();


    }
}
