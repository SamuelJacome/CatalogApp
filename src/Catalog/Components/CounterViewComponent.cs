using Catalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Components
{
    public class CounterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public CounterViewComponent(AppDbContext context)
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