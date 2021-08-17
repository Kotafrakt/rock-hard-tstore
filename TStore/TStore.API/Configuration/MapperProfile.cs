using AutoMapper;
using TStore.API.Models;
using TStore.DAL.Models;

namespace TStore.API.Configuration
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
