using AutoMapper;
using Real_State_Catalog.Core;
using Real_State_Catalog.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Repository
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<AmenityCreateDto, Amenity>();
            CreateMap<Amenity, AmenityCreateDto>();
        }
    }
}
