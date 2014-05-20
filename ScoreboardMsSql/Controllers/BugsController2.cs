using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{ 
    public class BugsController2 : Controller
    {
        private ScoreboardContext db = new ScoreboardContext();

        //
        // GET: /Bugs/

        public ViewResult Index()
        {
            var scoreboardbugs = db.ScoreBoardBugs.Include(s => s.Awards);
            return View(scoreboardbugs.ToList());
        }

        //
        // GET: /Bugs/Details/5

        public ViewResult Details(int id)
        {
            ScoreboardBugs scoreboardbugs = db.ScoreBoardBugs.Find(id);
            return View(scoreboardbugs);
        }

        //
        // GET: /Bugs/Create

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.ScoreBoardAwardsBAwards, "Id", "Id");
            return View();
        } 

        //
        // POST: /Bugs/Create

        [HttpPost]
        public ActionResult Create(ScoreboardBugs scoreboardbugs)
        {
            if (ModelState.IsValid)
            {
                db.ScoreBoardBugs.Add(scoreboardbugs);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Id = new SelectList(db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }
        
        //
        // GET: /Bugs/Edit/5
 
        public ActionResult Edit(int id)
        {
            ScoreboardBugs scoreboardbugs = db.ScoreBoardBugs.Find(id);
            ViewBag.Id = new SelectList(db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }

        //
        // POST: /Bugs/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreboardBugs scoreboardbugs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoreboardbugs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.ScoreBoardAwardsBAwards, "Id", "Id", scoreboardbugs.Id);
            return View(scoreboardbugs);
        }

        //
        // GET: /Bugs/Delete/5
 
        public ActionResult Delete(int id)
        {
            ScoreboardBugs scoreboardbugs = db.ScoreBoardBugs.Find(id);
            return View(scoreboardbugs);
        }

        //
        // POST: /Bugs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ScoreboardBugs scoreboardbugs = db.ScoreBoardBugs.Find(id);
            db.ScoreBoardBugs.Remove(scoreboardbugs);
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