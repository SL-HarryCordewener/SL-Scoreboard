using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers.Admin
{
    public class UsersController : Controller
    {
        private readonly ScoreboardContext _db = new ScoreboardContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(_db.ScoreBoardUsers.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardUsers scoreboardusers = _db.ScoreBoardUsers.Find(id);
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
                _db.ScoreBoardUsers.Add(scoreboardusers);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoreboardusers);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id)
        {
            ScoreboardUsers scoreboardusers = _db.ScoreBoardUsers.Find(id);
            return View(scoreboardusers);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreboardUsers scoreboardusers)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(scoreboardusers).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scoreboardusers);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id)
        {
            ScoreboardUsers scoreboardusers = _db.ScoreBoardUsers.Find(id);
            return View(scoreboardusers);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoreboardUsers scoreboardusers = _db.ScoreBoardUsers.Find(id);
            _db.ScoreBoardUsers.Remove(scoreboardusers);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}