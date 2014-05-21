using System.Web.Mvc;

namespace ScoreboardMsSql.Controllers
{
    /// <summary>
    /// This could just as easily be a page in the /Home directory.
    /// Its only purpose is to link to the Points, Awards and Users admin pages.
    /// A better way of doing this is perhaps in order - so we can pull those three into a Admin Controller.
    /// 
    /// This is the placeholder for that logic.
    /// </summary>
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
