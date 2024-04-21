using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restoran.Entities;
using Restoran.Data;

namespace Restoran.Data
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        private readonly RestoranContext context;
        private readonly IMapper mapper;

        public ZaposleniRepository(RestoranContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
        public List<Zaposleni> GetZaposleni()
        {
            return context.Zaposleni.ToList();
        }
        public Zaposleni GetZaposleniById(Guid ZaposleniID)
        {
            return context.Zaposleni.FirstOrDefault(e => e.ZaposleniID == ZaposleniID);
        }
        public Zaposleni CreateZaposleni(Zaposleni zaposleni)
        {
            var createdEntity = context.Zaposleni.Add(zaposleni);
            return mapper.Map<Zaposleni>(createdEntity.Entity);
        }

        public void DeleteZaposleni(Guid ZaposleniID)
        {
            var zaposleni = GetZaposleniById(ZaposleniID);
            context.Remove(zaposleni);
        }

        public Zaposleni UpdateZaposleni(Zaposleni zaposleni)
        {
            var existingZaposleni = context.Zaposleni.FirstOrDefault(e => e.ZaposleniID == zaposleni.ZaposleniID);
            try
            {
                existingZaposleni.ZaposleniID = zaposleni.ZaposleniID; ;
                existingZaposleni.ImeZaposlenog = zaposleni.ImeZaposlenog;
                existingZaposleni.PrezimeZaposlenog = zaposleni.PrezimeZaposlenog;
                existingZaposleni.AdresaZaposlenog = zaposleni.AdresaZaposlenog;
                existingZaposleni.GradZaposlenog = zaposleni.GradZaposlenog;
                existingZaposleni.KontaktZaposlenog = zaposleni.KontaktZaposlenog;

                context.SaveChanges();
                return existingZaposleni;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

       
    }
}
