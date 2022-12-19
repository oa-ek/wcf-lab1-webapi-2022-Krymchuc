using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Repository.IRepository
{
    public interface IImagesRepository  
    {

        public Task<int> CreateRoomImage(RoomImageDTO image);

        public Task<int> DeleteRoomImageByImageId(int imageId);
        public Task<int> DeleteRoomImageByRoomId(int roomId);
        public Task<IEnumerable<RoomImageDTO>> GetRoomImages(int roomId);

        public Task<int> DeleteImageByImageUrl(string imageUrl);

    }
}
