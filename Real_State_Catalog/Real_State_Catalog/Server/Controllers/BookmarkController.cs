using Real_State_Catalog.Core;
using Real_State_Catalog.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AppContext = Real_State_Catalog.Core.AppContext;

namespace Real_State_Catalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {     
        private readonly AppContext _context;
        private readonly string _userId;

        public BookmarkController(AppContext context, string userId)
        {
            _context = context;
            _userId = userId;
        }

        /// <summary>
        /// Method adds a bookmark
        /// </summary>
        [HttpPut("{id}")]
        public async Task Add(int offerId)
        {
            // Перевірте, чи вже існує закладка для підключеного користувача
            if (BookmarkExists(offerId) == null)
            {
                Bookmark bookmark = new();
                bookmark.OfferId = offerId;
                bookmark.UserId = _userId;

                await _context.Bookmarks.AddAsync(bookmark);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Method deletes a bookmark
        /// </summary>
        [HttpDelete("{id}")]
        public async Task Delete(int offerId)
        {
            var bookmark = BookmarkExists(offerId);

            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Method checks for a bookmark
        /// </summary>
        private Bookmark BookmarkExists(int offerId)
        {
            return _context.Bookmarks.Where(b => b.OfferId == offerId && b.UserId == _userId).SingleOrDefault();
        }

    }
}