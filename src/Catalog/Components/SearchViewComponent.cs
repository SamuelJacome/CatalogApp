using Catalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Components
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SearchViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int modelCounter)
        {
            var productDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync();

            return View(modelCounter);
        }
    }
}