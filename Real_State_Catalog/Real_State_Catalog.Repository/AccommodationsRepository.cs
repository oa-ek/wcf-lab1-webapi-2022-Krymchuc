using Microsoft.EntityFrameworkCore;
using Real_State_Catalog.Core;
using Real_State_Catalog.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AppContext = Real_State_Catalog.Core.AppContext;

namespace Real_State_Catalog.Repository
{
    public class AccommodationsRepository
    {
        private readonly AppContext _ctx;
        public AccommodationsRepository(AppContext _ctx)
        {
            this._ctx = _ctx;
        }

        public async Task<AccommodationCreateDto> AddKeyAsync(Accommodation ac)
        {
            _ctx.Accommodations.Add(ac);
            await _ctx.SaveChangesAsync();
            var createdAccommodation =  _ctx.Accommodations.Include(x => x.Amenity).FirstOrDefault(x => x.Name == ac.Name);
            return new AccommodationCreateDto
            {
                Id = createdAccommodation.Id,
                Name = createdAccommodation.Name,
                CountOfRooms = createdAccommodation.CountOfRooms,
                Type = createdAccommodation.Type,   
                MaxTraveler = createdAccommodation.MaxTraveler,
                Description = createdAccommodation.Description,
                ImagePath = createdAccommodation.ImagePath,
                Amenity = createdAccommodation.Amenity.Name,
                Adress = createdAccommodation.Adress,
                Price = createdAccommodation.Price
            };
        }

        public Accommodation GetAccommodation(int id)
        {
            return _ctx.Accommodations.Include(x => x.Amenity).FirstOrDefault(x => x.Id == id);
        }

        public List<AccommodationCreateDto> GetAccommodationDto()
        {
            var accomodationList = _ctx.Accommodations.Include(x => x.Amenity).ToList();
            var accomodationListDto = new List<AccommodationCreateDto>();
            foreach (var accomodation in accomodationList) 
            {
                accomodationListDto.Add(new AccommodationCreateDto
                {
                    Id = accomodation.Id,
                    Name = accomodation.Name,
                    CountOfRooms = accomodation.CountOfRooms,
                    Type = accomodation.Type,
                    MaxTraveler = accomodation.MaxTraveler,
                    Description = accomodation.Description,
                    ImagePath = accomodation.ImagePath,
                    Amenity = accomodation.Amenity.Name,
                    Adress = accomodation.Adress,
                    Price = accomodation.Price
                });
            }
            return accomodationListDto;
        }

        public List<Accommodation> GetAccommodations()
        {
            var keyList = _ctx.Accommodations.Include(x => x.Amenity).ToList();
            return keyList;
        }

        public async Task DeleteKeyAsync(int id)
        {
            _ctx.Remove(GetAccommodation(id));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAccommodationAsync(Accommodation updatedAccommodation)
        {
            var accommodation = _ctx.Accommodations.FirstOrDefault(x => x.Id == updatedAccommodation.Id);
            accommodation.Name = updatedAccommodation.Name;
            accommodation.CountOfRooms = updatedAccommodation.CountOfRooms;
            accommodation.MaxTraveler = updatedAccommodation.MaxTraveler;
            accommodation.Type= updatedAccommodation.Type;
            accommodation.Description= updatedAccommodation.Description; 
            accommodation.Price = updatedAccommodation.Price;
            accommodation.ImagePath= updatedAccommodation.ImagePath; 
            accommodation.Adress= updatedAccommodation.Adress;
            accommodation.Amenity= updatedAccommodation.Amenity;
            await _ctx.SaveChangesAsync();
        }

        public async Task<AccommodationCreateDto> GetKeyDto(int id)
        {
            var ac = await _ctx.Accommodations.Include(x => x.Amenity).FirstAsync(x => x.Id == id);

            var acDto = new AccommodationCreateDto
            {
                Id = ac.Id,
                Name = ac.Name,
                CountOfRooms = ac.CountOfRooms,
                Type = ac.Type,
                MaxTraveler = ac.MaxTraveler,
                Description = ac.Description,
                ImagePath = ac.ImagePath,
                Amenity = ac.Amenity.Name,
                Adress = ac.Adress,
                Price = ac.Price
            };
            return acDto;
        }

        public async Task UpdateAsync(AccommodationCreateDto model)
        {
            var accommodation = _ctx.Accommodations.Include(x => x.Amenity).FirstOrDefault(x => x.Id == model.Id);
            if (accommodation.Name != model.Name)
                accommodation.Name = model.Name;
            if (accommodation.Description != model.Description)
                accommodation.Description = model.Description;
            if (accommodation.CountOfRooms != model.CountOfRooms)
                accommodation.CountOfRooms = model.CountOfRooms;
            if (accommodation.Type != model.Type)
                accommodation.Type = model.Type;
            if (accommodation.MaxTraveler != model.MaxTraveler)
                accommodation.MaxTraveler = model.MaxTraveler;
            if(accommodation.ImagePath != model.ImagePath)
                accommodation.ImagePath = model.ImagePath;
            if (accommodation.Price != model.Price)
                accommodation.Price = model.Price;
            if(accommodation.Adress != model.Adress)
                accommodation.Adress = model.Adress;
            if(accommodation.Amenity.Name != model.Amenity)
                accommodation.Amenity=_ctx.Amenity.FirstOrDefault(x=>x.Name==model.Amenity);
            _ctx.SaveChanges();
        }
        public List<AccommodationCreateDto> SearchAccommodation(string searchText)
        {
            return GetAccommodationDto().
                Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                || x.Description.ToLower().Contains(searchText.ToLower())
                || x.Price.ToString().Contains(searchText.ToLower())
                || x.Adress.ToString().Contains(searchText.ToLower())       
                || x.Amenity.ToLower().Contains(searchText.ToLower())).ToList();
        }
    }
}