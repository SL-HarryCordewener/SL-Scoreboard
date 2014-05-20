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

        public ViewResult Index()
        {
            var scoreboardawardsbawards = db.ScoreBoardAwardsBAwards.Include(s => s.AwardBug).Include(u => u.AwardUser);
            return View(scoreboardawardsbawards.ToList());
        }

        //
        // GET: /Awards/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        //
        // GET: /Awards/Create

        private void PopulateUserList(object selecterUser = null)
        {
            var userQuery = from d in db.ScoreBoardUsers select d;
            ViewData["AwardUser"] = new SelectList(userQuery, "Id", "Name", selecterUser);
        }

        public ActionResult Create()
        {
            PopulateUserList();
            ViewData["AwardBug"] = new SelectList(db.ScoreBoardBugs, "Id", "Description");

            return View();
        } 

        //
        // POST: /Awards/Create

        [HttpPost]
        public ActionResult Create(ScoreboardAwards scoreboardawards)
        {
            //PopulateUserList(scoreboardawards.AwardUser);

            if (ModelState.IsValid)
            {
                db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            try
            {
                var realUser = (from user in db.ScoreBoardUsers where user.Id == scoreboardawards.AwardUser.Id select user).Single();
                var realBug = (from bug in db.ScoreBoardBugs where bug.Id == scoreboardawards.AwardBug.Id select bug).Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardBug = realBug;

                db.ScoreBoardAwardsBAwards.Add(scoreboardawards);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }


            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardBug"] = new SelectList(db.ScoreBoardBugs, "Id", "Description", scoreboardawards.AwardBug);
            ViewBag.Error = "Something went wrong.";

            // ViewBag.Id = new SelectList(db.ScoreBoardBugs, "Id", "Description");
            // ViewBag.Id = new SelectList(db.ScoreBoardAwardsBAwards, "Name", "Id", scoreboardawards.Id);

            return View(scoreboardawards);
        }
        
        //
        // GET: /Awards/Edit/5
 
        public ActionResult Edit(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardBug"] = new SelectList(db.ScoreBoardBugs, "Id", "Description", scoreboardawards.AwardBug);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Edit/5

        [HttpPost]
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
                var realBug = (from bug in db.ScoreBoardBugs where bug.Id == scoreboardawards.AwardBug.Id select bug).Single();

                scoreboardawards.AwardUser = realUser;
                scoreboardawards.AwardBug = realBug;

                db.Entry(scoreboardawards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went seriously wrong!";
            }

            ViewData["AwardUser"] = new SelectList(db.ScoreBoardUsers, "Id", "Name", scoreboardawards.AwardUser);
            ViewData["AwardBug"] = new SelectList(db.ScoreBoardBugs, "Id", "Description", scoreboardawards.AwardBug);
            return View(scoreboardawards);
        }

        //
        // GET: /Awards/Delete/5
 
        public ActionResult Delete(int id)
        {
            ScoreboardAwards scoreboardawards = db.ScoreBoardAwardsBAwards.Find(id);
            return View(scoreboardawards);
        }

        //
        // POST: /Awards/Delete/5

        [HttpPost, ActionName("Delete")]
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