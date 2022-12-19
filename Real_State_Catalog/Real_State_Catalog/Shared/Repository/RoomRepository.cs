using AutoMapper;
using Real_State_Catalog.Shared.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }


        public async Task<RoomDTO> CreateRoom(RoomDTO hotelRoomDTO)
        {
            Room hotelRoom = _mapper.Map<RoomDTO, Room>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";

            var addRoom = await _db.Rooms.AddAsync(hotelRoom);

            await _db.SaveChangesAsync();

            return _mapper.Map<Room, RoomDTO>(addRoom.Entity);

        }

        public async Task<IEnumerable<RoomDTO>> GetAllRoom(string checkInStr, string checkOutStr)
        {
            try
            {
                IEnumerable<RoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<Room>, 
                    IEnumerable<RoomDTO>>(_db.Rooms.Include(x => x.RoomImages));

                if (!string.IsNullOrEmpty(checkInStr) && !string.IsNullOrEmpty(checkOutStr)){
                   foreach(RoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsBooked(hotelRoom.Id, checkInStr, checkOutStr);

                    }

                }
                return hotelRoomDTOs;
            }

            catch (Exception ex )
            {
                {
                    throw ex;
                }
            }
        }


        public async Task<RoomDTO> GetRoom(int roomId, string checkInStr, string checkOutStr)
        {
            try
            {
                RoomDTO hotelRoom = _mapper.Map<Room, RoomDTO>(
                    await _db.Rooms.Include(x=>x.RoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if(!string.IsNullOrEmpty(checkInStr)&& !string.IsNullOrEmpty(checkOutStr)){
                    hotelRoom.IsBooked = await IsBooked(hotelRoom.Id, checkInStr, checkOutStr);
                }

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        
        public async Task<bool> IsBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "dd/MM/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "dd/MM/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.PaymentSuccessful &&
                       (checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate
                       || checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date
                       )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RoomDTO> UpdateRoom(int roomId, RoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    Room roomDetails = await _db.Rooms.FindAsync(roomId);
                    Room room = _mapper.Map<RoomDTO, Room>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.Rooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Room, RoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteRoom(int roomId)
        {
            var roomDetails = await _db.Rooms.FindAsync(roomId);

            if (roomDetails != null)
            {
                var allImages = await _db.RoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                _db.RoomImages.RemoveRange(allImages);
                _db.Rooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }
    }
}
