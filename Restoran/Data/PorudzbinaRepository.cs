using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restoran.Entities;
using Restoran.Data;

namespace Restoran.Data
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly RestoranContext context;
        private readonly IMapper mapper;

        public PorudzbinaRepository(RestoranContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public List<Porudzbina> GetPorudzbina()
        {
            return context.Porudzbina.ToList();
        }
        public Porudzbina GetPorudzbinaById(Guid PorudzbinaID)
        {
            return context.Porudzbina.FirstOrDefault(e => e.PorudzbinaID == PorudzbinaID);
        }
        public Porudzbina CreatePorudzbina(Porudzbina porudzbina)
        {
            var createdEntity = context.Porudzbina.Add(porudzbina);
            return mapper.Map<Porudzbina>(createdEntity.Entity);
        }

        public void DeletePorudzbina(Guid PorudzbinaID)
        {
            var porudzbina = GetPorudzbinaById(PorudzbinaID);
            context.Remove(porudzbina);
        }

        public Porudzbina UpdatePorudzbina(Porudzbina porudzbina)
        {
            var existingPorudzbina = context.Porudzbina.FirstOrDefault(e => e.PorudzbinaID == porudzbina.PorudzbinaID);
            try
            {
                existingPorudzbina.PorudzbinaID = porudzbina.PorudzbinaID; ;
                existingPorudzbina.IznosPorudzbine = porudzbina.IznosPorudzbine;
                existingPorudzbina.NacinPlacanja = porudzbina.NacinPlacanja;
                existingPorudzbina.DodeljeniSto = porudzbina.DodeljeniSto;
                existingPorudzbina.StatusPorudzbine = porudzbina.StatusPorudzbine;
                existingPorudzbina.MusterijaID = porudzbina.MusterijaID;
                existingPorudzbina.ZaposleniID = porudzbina.ZaposleniID;


                context.SaveChanges();
                return existingPorudzbina;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

       
    }
}
