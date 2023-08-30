using Microsoft.AspNetCore.Mvc;

namespace Catalog.Components
{
    public class SearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int modelCounter)
        {
            return View(modelCounter);
        }
    }
}