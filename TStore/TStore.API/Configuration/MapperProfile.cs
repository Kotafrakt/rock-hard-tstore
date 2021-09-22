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

        private const string format = "yyyy-MM-dd HH:mm:ss:fffffff";
        public MapperProfile()
        {
            CreateMap<TransactionInputModel, TransactionDto>()
                /*.ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.ParseExact(src.Date, _dateFormat, CultureInfo.InvariantCulture)))*/;
            CreateMap<TransferInputModel, TransferDto>()
                /*.ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.ParseExact(src.Date, _dateFormat, CultureInfo.InvariantCulture)))*/;
            CreateMap<GetByPeriodInputModel, GetByPeriodDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => DateTime.ParseExact(src.From, _dateFormat, CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => DateTime.ParseExact(src.To, _dateFormat, CultureInfo.InvariantCulture)));

            CreateMap<TransactionDto, TransactionOutputModel>();
            CreateMap<TransferDto, TransferOutputModel>();
        }
    }
}