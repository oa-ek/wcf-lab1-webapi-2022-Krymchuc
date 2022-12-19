using Real_State_Catalog.Core;
using Real_State_Catalog.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContext = Real_State_Catalog.Core.AppContext;

namespace Real_State_Catalog.Repository
{
    public class OffersRepository
    {
        private readonly AppContext _context;
        private readonly AccommodationsRepository keysRepository;

        public OffersRepository(AppContext context, AccommodationsRepository keysRepository)
        {
            this.keysRepository = keysRepository;
            _context = context;
        }

        public void CreateOffer(OfferDto orderDto)
        {
            var order = new Offer
            {
                Surname = orderDto.Surname,
                Name = orderDto.Name,
                Email = orderDto.Email,
                Phone = orderDto.Phone
            };
            order.DateTime = DateTime.Now;
            _context.Offers.Add(order);
            _context.SaveChanges();
           
        }
    }
}