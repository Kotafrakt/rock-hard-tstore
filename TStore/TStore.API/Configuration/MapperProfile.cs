using AutoMapper;
using TransactionStore.API.Models;
using TransactionStore.DAL.Models;

namespace TransactionStore.API.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TransactionInputModel, TransactionDto>();
            CreateMap<TransferInputModel, TransferDto>()
                .ForMember(dest => dest.SenderAmount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<TransactionDto, TransactionOutputModel>();
            CreateMap<TransferDto, TransferOutputModel>()
                   .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.SenderAmount));
        }
    }
}