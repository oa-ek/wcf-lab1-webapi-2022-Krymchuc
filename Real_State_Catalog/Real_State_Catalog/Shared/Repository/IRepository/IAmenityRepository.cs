using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Repository.IRepository
{
    public interface IAmenityRepository
    {

        public Task<AmenityDTO> CreateAmenity(AmenityDTO hotelAmenity);
        public Task<AmenityDTO> UpdateAmenity(int amenityId, AmenityDTO hotelAmenity);
        public Task<int> DeleteAmenity(int amenityId, string userId);
        public Task<IEnumerable<AmenityDTO>> GetAllAmenity();
        public Task<AmenityDTO> GetAmenity(int amenityId);
        public Task<AmenityDTO> IsSameNameAmenityAlreadyExists(string name);


    }
}
