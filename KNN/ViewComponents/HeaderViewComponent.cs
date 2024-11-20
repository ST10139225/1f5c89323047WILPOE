using Microsoft.AspNetCore.Mvc;

namespace KNN.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Factory.StartNew(() => { return View(); });
        }
    }
}
