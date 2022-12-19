using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Repository.IRepository
{
    public interface IRoomRepository
    {
        public Task<RoomDTO> CreateRoom(RoomDTO hotelRoomDTO);
        public Task<RoomDTO> UpdateRoom(int roomId,RoomDTO hotelRoomDTO);
        public Task<RoomDTO> GetRoom(int roomId, string checkIn = null, string checkOut = null);
        public Task<int> DeleteRoom(int roomId);
        public Task<IEnumerable<RoomDTO>> GetAllRoom(string checkIn = null, string checkOut = null);
        public Task<bool> IsBooked(int RoomId, string checkInDate, string checkOutDate);

    }
}
