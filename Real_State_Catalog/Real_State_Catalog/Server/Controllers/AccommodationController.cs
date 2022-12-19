using Real_State_Catalog.Core;
using Real_State_Catalog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Annotations;
using Real_State_Catalog.Shared.Dtos;

namespace Real_State_Catalog.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationController : ControllerBase
    {
        private readonly AccommodationsRepository _accommodationRepository;
        private readonly AmenityRepository _amenityRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccommodationController(AccommodationsRepository accommodationRepository, AmenityRepository amenityRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._accommodationRepository = accommodationRepository;
            _amenityRepository = amenityRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Method returnes list of accommodations from database
        /// </summary>
        /// 
        [HttpGet]
        public List<AccommodationCreateDto> GetAccomodations()
        {
            var keys = _accommodationRepository.GetAccommodationDto();
            return keys;
        }

        /// <summary>
        /// Method returnes accommodation from database by id
        /// </summary>
        /// <param name="id">id of searching accommodation</param>
        /// <returns>accommodation from database</returns>
        [HttpGet("{id}")]
        public AccommodationCreateDto GetKey(int id)
        {
            var accommodation = _accommodationRepository.GetAccommodation(id);
            var accommodationDto = new AccommodationCreateDto 
            {
                Id = accommodation.Id,
                Name = accommodation.Name,
                CountOfRooms = accommodation.CountOfRooms,
                Type = accommodation.Type,
                MaxTraveler = accommodation.MaxTraveler,
                Description = accommodation.Description,
                ImagePath = accommodation.ImagePath,
                Amenity =  accommodation.Amenity.Name,
                Adress = accommodation.Adress,
                Price = accommodation.Price
            };
            return accommodationDto;
        }

        /// <summary>
        /// Method creates accommodation and adds it to database
        /// </summary>
        [HttpPost]
        public async Task<AccommodationCreateDto> CreateAccommodation(AccommodationCreateDto accommodationCreateDto)
        {
            var amenity = _amenityRepository.GetAmenityByName(accommodationCreateDto.Amenity);
            if (amenity == null)
            {
                amenity = new Amenity() { Name = accommodationCreateDto.Amenity };
                amenity = await _amenityRepository.AddAmenityAsync(amenity);
            }

            var accommodation = await _accommodationRepository.AddAccommodationAsync(new Accommodation()
            {
                Name = accommodationCreateDto.Name,
                Description = accommodationCreateDto.Description,
                CountOfRooms = accommodationCreateDto.CountOfRooms,
                Type= accommodationCreateDto.Type,
                MaxTraveler=accommodationCreateDto.MaxTraveler,
                ImagePath=accommodationCreateDto.ImagePath,
                Adress=accommodationCreateDto.Adress,
                Price=accommodationCreateDto.Price,
                Amenity=amenity
            });
            return accommodation;
        }

        /// <summary>
        /// Method edits accommodation from database
        /// </summary>
        [HttpPut]
        public async Task Edit(AccommodationCreateDto model)
        {
            await _accommodationRepository.UpdateAsync(model);
        }

        /// <summary>
        /// Method deletes accommodation from database by id
        /// </summary>
        /// <param name="id">id of deleting accommodation</param>
        [HttpDelete("{id}")]
        public async Task ConfirmDelete(int id)
        {
            await _accommodationRepository.DeleteAccommodationAsync(id);
        }

        [HttpGet("search/{searchBy}")]
        public ActionResult<List<AccommodationCreateDto>> SearchAccommodations(string searchBy)
        {
            return Ok(_accommodationRepository.SearchAccommodation(searchBy));
        }
    }
}