using Catalog.Data;
using Catalog.Extension;
using Catalog.Model;
using Catalog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IImageUploadService _imageUploadService;

        public ProductsController(AppDbContext context, IImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;

        }
        public async Task<IActionResult> Index()
        {
            return _context.Products != null ?
                        View(await _context.Products.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Produtos'  is null.");
        }
        [ClaimsAuthorize("Product", "R")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var produto = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        [ClaimsAuthorize("Product", "C")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Product", "C")]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUpload,Value")] Product product)
        {
            if (ModelState.IsValid)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await _imageUploadService.UploadArquivo(ModelState, product?.ImageUpload, imgPrefixo))
                {
                    return View(product);
                }
                product.Image = imgPrefixo + product.ImageUpload.FileName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var produto = await _context.Products.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Product", "U")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUpload,Value")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var productDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    product.Image = productDb?.Image;
                    if (product.ImageUpload is not null)
                    {
                        var imgPrefixo = Guid.NewGuid() + "_";
                        if (!await _imageUploadService.UploadArquivo(ModelState, product?.ImageUpload, imgPrefixo))
                        {
                            return View(product);
                        }
                        product.Image = imgPrefixo + product.ImageUpload.FileName;
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [ClaimsAuthorize("Product", "D")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var produto = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ClaimsAuthorize("Product", "D")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _context.Products.FindAsync(id);
            if (produto != null)
            {
                _context.Products.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}