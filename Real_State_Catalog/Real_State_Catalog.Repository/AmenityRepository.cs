using AutoMapper;
using Real_State_Catalog.Core;
using Real_State_Catalog.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContext = Real_State_Catalog.Core.AppContext;

namespace Real_State_Catalog.Repository
{
    public class AmenityRepository
    {
        private readonly AppContext _ctx;
        private readonly IMapper _mapper;

        public AmenityRepository(AppContext _ctx, IMapper mapper)
        {
            this._ctx = _ctx;
            _mapper = mapper;   
        }

        public List<AmenityCreateDto> GetAmenity()
        {
            var amenityList = _ctx.Amenity.ToList();
            return _mapper.Map<List<AmenityCreateDto>>(_ctx.Amenity.ToList());
        }

        public async Task<Amenity> AddBrandByDtoAsync(AmenityCreateDto amenityDto)
        {
            var amenity = new Amenity();
            amenity.Name = amenityDto.Name;
            amenity.Description = amenityDto.Description;
            _ctx.Amenity.Add(amenity);
            await _ctx.SaveChangesAsync();
            return _ctx.Amenity.FirstOrDefault(x => x.Name == amenity.Name);
        }
        public async Task<Amenity> AddAmenityAsync(Amenity amenity)
        {
            _ctx.Amenity.Add(amenity);
            await _ctx.SaveChangesAsync();
            return _ctx.Amenity.FirstOrDefault(x => x.Name == amenity.Name);
        }

        public Amenity GetAmenity(int id)
        {
            return _ctx.Amenity.FirstOrDefault(x => x.Id == id);
        }
        public Amenity GetAmenityByName(string name)
        {
            return _ctx.Amenity.FirstOrDefault(x => x.Name == name);
        }
        
        public AmenityCreateDto GetBrandDtoByName(string name)
        {
            var amenity = _ctx.Amenity.FirstOrDefault(x => x.Name == name);
            var amenityDto = new AmenityCreateDto
            {
                Name = amenity.Name,
                Description = amenity.Description,
                Id = amenity.Id
            };

            return amenityDto;
        }



        public async Task DeleteAmenityAsync(int id)
        {
            _ctx.Remove(GetAmenity(id));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAmenityAsync(AmenityCreateDto updatedAmenity)
        {
            var brand = _ctx.Amenity.FirstOrDefault(x => x.Id == updatedAmenity.Id);
            brand.Description = updatedAmenity.Description;
            brand.Name = updatedAmenity.Name;
            await _ctx.SaveChangesAsync();
        }
    }
}