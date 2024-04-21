using Restoran.Entities;

namespace Restoran.Data
{
    public interface IPorudzbinaRepository
    {
        
        List<Porudzbina> GetPorudzbina();
        Porudzbina GetPorudzbinaById(Guid PorudzbinaID);
        Porudzbina CreatePorudzbina(Porudzbina porudzbina);
        Porudzbina UpdatePorudzbina(Porudzbina porudzbina);
        void DeletePorudzbina(Guid PorudzbinaID);
        bool SaveChanges();


    }
}
