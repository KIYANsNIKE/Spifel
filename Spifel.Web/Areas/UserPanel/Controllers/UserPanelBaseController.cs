using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spifel.Web.Controllers;

namespace Spifel.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserPanelBaseController : BaseController
    {
    }
}
