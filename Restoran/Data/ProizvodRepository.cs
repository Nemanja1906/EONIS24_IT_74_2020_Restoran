using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restoran.Entities;
using Restoran.Data;

namespace Restoran.Data
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly RestoranContext context;
        private readonly IMapper mapper;

        public ProizvodRepository(RestoranContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public List<Proizvod> GetProizvod()
        {
            return context.Proizvod.ToList();
        }
        public Proizvod GetProizvodById(Guid ProizvodID)
        {
            return context.Proizvod.FirstOrDefault(e => e.ProizvodID == ProizvodID);
        }
        public Proizvod CreateProizvod(Proizvod proizvod)
        {
            var createdEntity = context.Proizvod.Add(proizvod);
            return mapper.Map<Proizvod>(createdEntity.Entity);
        }

        public void DeleteProizvod(Guid ProizvodID)
        {
            var proizvod = GetProizvodById(ProizvodID);
            context.Remove(proizvod);
        }

        public Proizvod UpdateProizvod(Proizvod proizvod)
        {
            var existingProizvod = context.Proizvod.FirstOrDefault(e => e.ProizvodID == proizvod.ProizvodID);
            try
            {
                existingProizvod.ProizvodID = proizvod.ProizvodID; ;
                existingProizvod.Hrana = proizvod.Hrana;
                existingProizvod.Pice = proizvod.Pice;
                existingProizvod.NazivProizvoda = proizvod.NazivProizvoda;
                existingProizvod.Cena = proizvod.Cena;
                existingProizvod.Opis = proizvod.Opis;


                context.SaveChanges();
                return existingProizvod;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

       
    }
}
