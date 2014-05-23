using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers.Admin
{
    public class AwardsController : Controller
    {
        private readonly ScoreboardContext _db = new ScoreboardContext();

        //
        // GET: /Awards/

        public ViewResult Index()
        {
            IQueryable<ScoreboardAwards> scoreboardawardsbawards =
                _db.ScoreBoardAwardsBAwards.Include(s => s.AwardPoint).Include(u => u.AwardUser);
            return View(scoreboardawardsbawards.ToList());
        }

        //
        // GET: /Awards/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardAwards scoreboardawards = _db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        // This is an alternate way to fill out user list. This is just here for reference. Function jumps are expensive.
        private void PopulateUserList(object selecterUser = null)
        {
            IQueryable<ScoreboardUsers> userQuery = from d in _db.ScoreBoardUsers select d;
            ViewData["AwardUser"] = new SelectList(userQuery, "Id", "Name", selecterUser);
        }

        //
        // GET: /Awards/Create

        public ActionResult Create()
        {
            PopulateUserList();
            ViewData["AwardPoint"] = new SelectList(_db.ScoreBoardPoints, "Id", "Description");

            return View();
        }

        //
        // POST: /Awards/Create

        [HttpPost]
        public ActionResult Create(ScoreboardAwards scoreboardawards)
        {
            if (ModelState.IsValid)
            {
                _db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            try
            {
                ScoreboardUsers realUser =
                    (from user in _db.ScoreBoardUsers where user.Id == scoreboardawards.AwardUser.Id select user).Single();
                ScoreboardPoints realPoint =
                    (from point in _db.ScoreBoardPoints where point.Id == scoreboardawards.AwardPoint.Id select point)
                        .Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardPoint = realPoint;

                _db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }


            ViewData["AwardUser"] = new SelectList(_db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(_db.ScoreBoardPoints, "Id", "Description",
                scoreboardawards.AwardPoint);
            ViewBag.Error = "Something went wrong.";

            return View(scoreboardawards);
        }

        //
        // GET: /Awards/Edit/5

        public ActionResult Edit(int id)
        {
            ScoreboardAwards scoreboardawards = _db.ScoreBoardAwardsBAwards.Find(id);
            ViewData["AwardUser"] = new SelectList(_db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(_db.ScoreBoardPoints, "Id", "Description",
                scoreboardawards.AwardPoint);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreboardAwards scoreboardawards)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(scoreboardawards).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            try
            {
                ScoreboardUsers realUser =
                    (from user in _db.ScoreBoardUsers where user.Id == scoreboardawards.AwardUser.Id select user).Single();
                ScoreboardPoints realPoint =
                    (from point in _db.ScoreBoardPoints where point.Id == scoreboardawards.AwardPoint.Id select point)
                        .Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardPoint = realPoint;

                _db.Entry(scoreboardawards).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }

            ViewData["AwardUser"] = new SelectList(_db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(_db.ScoreBoardPoints, "Id", "Description",
                scoreboardawards.AwardPoint);
            return View(scoreboardawards);
        }

        //
        // GET: /Awards/Delete/5

        public ActionResult Delete(int id)
        {
            ScoreboardAwards scoreboardawards = _db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoreboardAwards scoreboardawards = _db.ScoreBoardAwardsBAwards.Find(id);
            _db.ScoreBoardAwardsBAwards.Remove(scoreboardawards);
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