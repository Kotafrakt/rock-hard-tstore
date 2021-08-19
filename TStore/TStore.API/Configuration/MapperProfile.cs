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
            CreateMap<TransactionDto, TransactionOutputModel>();
        }
    }
}
