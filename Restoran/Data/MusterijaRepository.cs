using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restoran.Entities;
using Restoran.Data;

namespace Restoran.Data
{
    public class MusterijaRepository : IMusterijaRepository
    {
        private readonly RestoranContext context;
        private readonly IMapper mapper;

        public MusterijaRepository(RestoranContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public List<Musterija> GetMusterija()
        {
            return context.Musterija.ToList();
        }
        public Musterija GetMusterijaById(Guid MusterijaID)
        {
            return context.Musterija.FirstOrDefault(e => e.MusterijaID == MusterijaID);
        }
        public Musterija CreateMusterija(Musterija musterija)
        {
            var createdEntity = context.Musterija.Add(musterija);
            return mapper.Map<Musterija>(createdEntity.Entity);
        }

        public void DeleteMusterija(Guid MusterijaID)
        {
            var musterija = GetMusterijaById(MusterijaID);
            context.Remove(musterija);
        }

        public Musterija UpdateMusterija(Musterija musterija)
        {
            var existingMusterija = context.Musterija.FirstOrDefault(e => e.MusterijaID == musterija.MusterijaID);
            try
            {
                existingMusterija.MusterijaID = musterija.MusterijaID; ;
                existingMusterija.ImeMusterije = musterija.ImeMusterije;
                existingMusterija.PrezimeMusterije = musterija.PrezimeMusterije;
                existingMusterija.AdresaMusterije = musterija.AdresaMusterije;
                existingMusterija.GradMusterije = musterija.GradMusterije;
                existingMusterija.KontaktMusterije = musterija.KontaktMusterije;


                context.SaveChanges();
                return existingMusterija;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

       
    }
}
