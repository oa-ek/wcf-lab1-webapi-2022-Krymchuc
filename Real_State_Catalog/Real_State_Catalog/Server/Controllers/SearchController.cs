using Real_State_Catalog.Core;
using Real_State_Catalog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Real_State_Catalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly AccommodationsRepository _accommodationRepository;

        public SearchController(AccommodationsRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        /// <summary>
        /// Method searches accommodation in database and returns the list of them
        /// </summary>
        /// <returns>list of accommodations</returns>
        [HttpGet("searchAccommodations/{acname}")]
        public async Task<List<Accommodation>> SearchAccommodation(string acname)
        {
            if (String.IsNullOrEmpty(acname))
            {
                return null;
            }
            var list = _accommodationRepository.GetAccommodations();
            list = list.Where(s => s.Name!.ToLower().Contains(acname.ToLower())).ToList();
            return list;
                
        }
    }
}