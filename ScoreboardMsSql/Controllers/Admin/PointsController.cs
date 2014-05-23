using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers.Admin
{
    public class PointsController : Controller
    {
        private readonly ScoreboardContext _db = new ScoreboardContext();

        //
        // GET: /Points/

        public ViewResult Index()
        {
            IQueryable<ScoreboardPoints> scoreboardbugs = _db.ScoreBoardPoints.Include(s => s.Awards);
            return View(scoreboardbugs.ToList());
        }

        //
        // GET: /Points/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardPoints scoreboardbugs = _db.ScoreBoardPoints.Find(id);
            return View(scoreboardbugs);
        }

        //
        // GET: /Points/Create

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(_db.ScoreBoardAwardsBAwards, "Id", "Id");
            return View();
        }

        //
        // POST: /Points/Create

        [HttpPost]
        public ActionResult Create(ScoreboardPoints scoreboardbugs)
        {
            if (ModelState.IsValid)
            {
                _db.ScoreBoardPoints.Add(scoreboardbugs);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(_db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }

        //
        // GET: /Points/Edit/5

        public ActionResult Edit(int id)
        {
            ScoreboardPoints scoreboardbugs = _db.ScoreBoardPoints.Find(id);
            ViewBag.Id = new SelectList(_db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }

        //
        // POST: /Points/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreboardPoints scoreboardbugs)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(scoreboardbugs).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }

        //
        // GET: /Points/Delete/5

        public ActionResult Delete(int id)
        {
            ScoreboardPoints scoreboardbugs = _db.ScoreBoardPoints.Find(id);
            return View(scoreboardbugs);
        }

        //
        // POST: /Points/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoreboardPoints scoreboardbugs = _db.ScoreBoardPoints.Find(id);
            _db.ScoreBoardPoints.Remove(scoreboardbugs);
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