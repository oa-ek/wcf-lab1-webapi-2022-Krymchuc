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
    public class ImagesRepository : IImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }


        public async Task<int> CreateRoomImage(RoomImageDTO imageDTO)
        {

            var image = _mapper.Map<RoomImageDTO, RoomImage>(imageDTO);
            await _db.RoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.RoomImages.FirstOrDefaultAsync(x => x.ImageURL.ToLower() == imageUrl.ToLower());
            _db.RoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomImageByImageId(int imageId)
        {
            var image = await _db.RoomImages.FindAsync(imageId);
            _db.RoomImages.Remove(image);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> DeleteRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.RoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.RoomImages.RemoveRange(imageList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomImageDTO>> GetRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<RoomImage>, IEnumerable<RoomImageDTO>>(
            await _db.RoomImages.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
