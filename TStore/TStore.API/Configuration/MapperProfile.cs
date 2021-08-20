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
            CreateMap<TransferInputModel, TransferDto>();
            CreateMap<TransactionDto, TransactionOutputModel>();
            CreateMap<TransferDto, TransferOutputModel>();
        }
    }
}
