using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
