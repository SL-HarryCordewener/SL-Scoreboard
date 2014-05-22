using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{
    public class UsersController : Controller
    {
        private readonly ScoreboardContext db = new ScoreboardContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(db.ScoreBoardUsers.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardUsers scoreboardusers = db.ScoreBoardUsers.Find(id);
            return View(scoreboardusers);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(ScoreboardUsers scoreboardusers)
        {
            if (ModelState.IsValid)
            {
                db.ScoreBoardUsers.Add(scoreboardusers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoreboardusers);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id)
        {
            ScoreboardUsers scoreboardusers = db.ScoreBoardUsers.Find(id);
            return View(scoreboardusers);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreboardUsers scoreboardusers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoreboardusers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scoreboardusers);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id)
        {
            ScoreboardUsers scoreboardusers = db.ScoreBoardUsers.Find(id);
            return View(scoreboardusers);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoreboardUsers scoreboardusers = db.ScoreBoardUsers.Find(id);
            db.ScoreBoardUsers.Remove(scoreboardusers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}