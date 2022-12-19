using AutoMapper;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomImage, RoomImageDTO>().ReverseMap();
            CreateMap<Amenity, AmenityDTO>();
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x => x.RoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
        }
    }
}
