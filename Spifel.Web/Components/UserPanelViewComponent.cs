using Microsoft.AspNetCore.Mvc;

namespace Spifel.Web.Components
{
    public class UserPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
