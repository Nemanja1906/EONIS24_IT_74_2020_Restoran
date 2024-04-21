using AutoMapper;
using Restoran.Dto;
using Restoran.Entities;


namespace Restoran
{
    public class Automapper : Profile
    {
        public Automapper()
        { 
            CreateMap<Proizvod, ProizvodDto>().ReverseMap();           
            CreateMap<Porudzbina, PorudzbinaDto>().ReverseMap();
            CreateMap<StavkaPorudzbine, StavkaPorudzbineDto>().ReverseMap();        
            CreateMap<Musterija, MusterijaDto>().ReverseMap();
            CreateMap<Zaposleni, ZaposleniDto>().ReverseMap();


        }
    }
}
