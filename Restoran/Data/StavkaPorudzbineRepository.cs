using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restoran.Entities;
using Restoran.Data;

namespace Restoran.Data
{
    public class StavkaPorudzbineRepository : IStavkaPorudzbineRepository
    {
        private readonly RestoranContext context;
        private readonly IMapper mapper;

        public StavkaPorudzbineRepository(RestoranContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public List<StavkaPorudzbine> GetStavkaPorudzbine()
        {
            return context.StavkaPorudzbine.ToList();
        }
        public StavkaPorudzbine GetStavkaPorudzbineById(Guid PorudzbinaID)
        {
            return context.StavkaPorudzbine.FirstOrDefault(e => e.PorudzbinaID == PorudzbinaID);
        }
        public StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine)
        {
            var createdEntity = context.StavkaPorudzbine.Add(stavkaPorudzbine);
            return mapper.Map<StavkaPorudzbine>(createdEntity.Entity);
        }

        public void DeleteStavkaPorudzbine(Guid PorudzbinaID)
        {
            var stavkaPorudzbine = GetStavkaPorudzbineById(PorudzbinaID);
            context.Remove(stavkaPorudzbine);
        }

        public StavkaPorudzbine UpdateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine)
        {
            var existingStavkaPorudzbine = context.StavkaPorudzbine.FirstOrDefault(e => e.PorudzbinaID == stavkaPorudzbine.PorudzbinaID);
            try
            {
                existingStavkaPorudzbine.StavkaPorudzbineID = stavkaPorudzbine.StavkaPorudzbineID;
                existingStavkaPorudzbine.PorudzbinaID = stavkaPorudzbine.PorudzbinaID;
                existingStavkaPorudzbine.ProizvodID = stavkaPorudzbine.ProizvodID;
                existingStavkaPorudzbine.Kolicina = stavkaPorudzbine.Kolicina;



                context.SaveChanges();
                return existingStavkaPorudzbine;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

       
    }
}
