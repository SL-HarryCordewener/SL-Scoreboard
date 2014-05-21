using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{ 
    public class AwardsController : Controller
    {
        private ScoreboardContext db = new ScoreboardContext();

        //
        // GET: /Awards/
        [Authorize, ValidateAntiForgeryToken]
        public ViewResult Index()
        {
            var scoreboardawardsbawards = db.ScoreBoardAwardsBAwards.Include(s => s.AwardPoint).Include(u => u.AwardUser);
            return View(scoreboardawardsbawards.ToList());
        }

        //
        // GET: /Awards/Details/5
        [Authorize, ValidateAntiForgeryToken]
        public ViewResult Details(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        // This is an alternate way to fill out user list. This is just here for reference. Function jumps are expensive.
        private void PopulateUserList(object selecterUser = null)
        {
            var userQuery = from d in db.ScoreBoardUsers select d;
            ViewData["AwardUser"] = new SelectList(userQuery, "Id", "Name", selecterUser);
        }

        //
        // GET: /Awards/Create
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            PopulateUserList();
            ViewData["AwardPoint"] = new SelectList(db.ScoreBoardPoints, "Id", "Description");

            return View();
        } 

        //
        // POST: /Awards/Create
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(ScoreboardAwards scoreboardawards)
        {
            if (ModelState.IsValid)
            {
                db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            try
            {
                var realUser = (from user in db.ScoreBoardUsers where user.Id == scoreboardawards.AwardUser.Id select user).Single();
                var realPoint = (from point in db.ScoreBoardPoints where point.Id == scoreboardawards.AwardPoint.Id select point).Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardPoint = realPoint;

                db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }


            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(db.ScoreBoardPoints, "Id", "Description", scoreboardawards.AwardPoint);
            ViewBag.Error = "Something went wrong.";

            return View(scoreboardawards);
        }
        
        //
        // GET: /Awards/Edit/5
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(db.ScoreBoardPoints, "Id", "Description", scoreboardawards.AwardPoint);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Edit/5

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(ScoreboardAwards scoreboardawards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoreboardawards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            try
            {
                var realUser = (from user in db.ScoreBoardUsers where user.Id == scoreboardawards.AwardUser.Id select user).Single();
                var realPoint = (from point in db.ScoreBoardPoints where point.Id == scoreboardawards.AwardPoint.Id select point).Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardPoint = realPoint;

                db.Entry(scoreboardawards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }

            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardPoint"] = new SelectList(db.ScoreBoardPoints, "Id", "Description", scoreboardawards.AwardPoint);
            return View(scoreboardawards);
        }

        //
        // GET: /Awards/Delete/5
        [Authorize, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete"), Authorize, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            db.ScoreBoardAwardsBAwards.Remove(scoreboardawards);
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