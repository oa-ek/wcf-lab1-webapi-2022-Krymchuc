using Real_State_Catalog.Shared.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace Real_State_Catalog_API.Controllers
{


    [Route("api/[controller]")]
    public class AmenityController : Controller
    {
        private readonly IAmenityRepository _roomAmenityRepository;

        public AmenityController( IAmenityRepository hotelAmenityRepository)
        {
            _roomAmenityRepository = hotelAmenityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAmenityList()
        {
            var amenityList = await _roomAmenityRepository.GetAllAmenity();
            return Ok(amenityList);
    
        } 
    }
}
