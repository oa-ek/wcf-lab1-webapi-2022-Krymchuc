using Real_State_Catalog.Core;
using Real_State_Catalog.Repository;
using Real_State_Catalog.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Real_State_Catalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenityController : ControllerBase
    {
        private readonly AmenityRepository _amenityRepository;
        public AmenityController(AmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        /// <summary>
        /// Method returns list of amenities
        /// </summary>
        /// <returns>array of amenities</returns>
        [HttpGet]
        public List<AmenityCreateDto> GetAmenities()
        {
            var amenities = _amenityRepository.GetAmenity();
            return amenities;
        }


        /// <summary>
        /// Method takes amenity from database
        /// </summary>
        /// <param name="id">id of searching amenity</param>
        /// <returns>amenity from db</returns>
        [HttpGet("{id}")]
        public Amenity GetAmenity(int id)
        {
            return _amenityRepository.GetAmenity(id);
        }

        /// <summary>
        /// Method creates amenity and adds it to database
        /// </summary>
        /// <returns>created amenity from database</returns>
        [HttpPost]
        public async Task<Amenity> Create(AmenityCreateDto amenityDto)
        {
            var createdAmenity = await _amenityRepository.AddAmenityByDtoAsync(amenityDto);
            return createdAmenity;
        }

        /// <summary>
        /// Method updates amenity in database
        /// </summary>
        [HttpPut]
        public async Task Edit(AmenityCreateDto amenity)
        {
            await _amenityRepository.UpdateAmenityAsync(amenity);
        }

        /// <summary>
        /// Method deletes amenity
        /// </summary>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _amenityRepository.DeleteAmenityAsync(id);
        }
    }
}