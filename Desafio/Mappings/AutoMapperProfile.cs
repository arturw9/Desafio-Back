using AutoMapper;
using Desafio.Models;
using Desafio.ViewModels;

namespace Desafio.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {           
            CreateMap<TituloViewModel, TituloModel>();
            CreateMap<ParcelaViewModel, ParcelaModel>();

            CreateMap<TituloModel, TituloViewModel>();
            CreateMap<ParcelaModel, ParcelaViewModel>();
        }
    }
}