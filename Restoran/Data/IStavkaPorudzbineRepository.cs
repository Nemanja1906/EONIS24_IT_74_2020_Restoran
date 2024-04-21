using Restoran.Entities;

namespace Restoran.Data
{
    public interface IStavkaPorudzbineRepository
    {
        
        List<StavkaPorudzbine> GetStavkaPorudzbine();
        StavkaPorudzbine GetStavkaPorudzbineById(Guid PorudzbinaID);
        StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine);
        StavkaPorudzbine UpdateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine);
        void DeleteStavkaPorudzbine(Guid PorudzbinaID);
        bool SaveChanges();


    }
}
