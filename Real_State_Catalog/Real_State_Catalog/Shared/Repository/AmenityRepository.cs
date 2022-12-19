using AutoMapper;
using Real_State_Catalog.Shared.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Repository
{
    public class AmenityRepository : IAmenityRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AmenityRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<AmenityDTO> CreateAmenity(AmenityDTO roomAmenity)
        {
            var amenity = _mapper.Map<AmenityDTO, Amenity>(roomAmenity);
            amenity.CreatedBy = "";
            amenity.CreatedDate = DateTime.UtcNow;
            var addedRoomAmenity = await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<Amenity, AmenityDTO>(addedRoomAmenity.Entity);
        }

        public async Task<AmenityDTO> UpdateAmenity(int amenityId, AmenityDTO roomAmenity)
        {
            var amenityDetails = await _context.Amenities.FindAsync(amenityId);
            var amenity = _mapper.Map(roomAmenity, amenityDetails);
            amenity.UpdatedBy = "";
            amenity.UpdatedDate = DateTime.UtcNow;
            var updatedamenity = _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<Amenity, AmenityDTO>(updatedamenity.Entity);
        }

        public async Task<int> DeleteAmenity(int amenityId, string userId)
        {
            var amenityDetails = await _context.Amenities.FindAsync(amenityId);
            if (amenityDetails != null)
            {
                _context.Amenities.Remove(amenityDetails);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<IEnumerable<AmenityDTO>> GetAllAmenity()
        {
            return _mapper.Map<IEnumerable<Amenity>, IEnumerable<AmenityDTO>>(await _context.Amenities.ToListAsync());
        }

        public async Task<AmenityDTO> GetAmenity(int amenityId)
        {
            var amenityData = await _context.Amenities
                           .FirstOrDefaultAsync(x => x.Id == amenityId);

            if (amenityData == null)
            {
                return null;
            }
            return _mapper.Map<Amenity, AmenityDTO>(amenityData);
        }

        public async Task<AmenityDTO> IsSameNameAmenityAlreadyExists(string name)
        {
            try
            {
                var amenityDetails =
                    await _context.Amenities.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim()
                    );
                return _mapper.Map<Amenity, AmenityDTO>(amenityDetails);
            }
            catch (Exception ex)
            {

            }
            return new AmenityDTO();
        }
    }
}
