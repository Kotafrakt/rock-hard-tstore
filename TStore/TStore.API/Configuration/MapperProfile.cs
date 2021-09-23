using AutoMapper;
using System;
using System.Globalization;
using TransactionStore.API.Models;
using TransactionStore.DAL.Models;

namespace TransactionStore.API.Configuration
{
    public class MapperProfile : Profile
    {
        private const string _dateFormat = "dd.MM.yyyy HH:mm";

        public MapperProfile()
        {
            CreateMap<TransactionInputModel, TransactionDto>();
            CreateMap<TransferInputModel, TransferDto>();
            CreateMap<GetByPeriodInputModel, GetByPeriodDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => DateTime.ParseExact(src.From, _dateFormat, CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => DateTime.ParseExact(src.To, _dateFormat, CultureInfo.InvariantCulture)));
        }
    }
}