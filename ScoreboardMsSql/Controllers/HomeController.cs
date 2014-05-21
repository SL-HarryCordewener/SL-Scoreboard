using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScoreboardContext db = new ScoreboardContext();

        public class ResultItems
        {
            public string name;

            public int pos;
            public int low;
            public int mid;
            public int high;
            public int total;
        }

        public ActionResult Index()
        {
            // var scoreboardawardsbawards = db.ScoreBoardAwardsBAwards.Include(award => award.AwardPoint).Include(award => award.AwardUser);
            var scoreboardawardsbawards = from awards in db.ScoreBoardAwardsBAwards
                where (awards.AwardTime.Year == DateTime.Now.Year)
                group awards by new {awards.AwardUser.Id, awards.AwardUser.Name, awards.AwardPoint.Points}
                into rez
                select new ResultItems {
                    name = rez.Key.Name,
                    low = rez.Sum(awards => (awards.AwardPoint.Points >= 0 && awards.AwardPoint.Points < 2 ? 1 : 0)),
                    mid = rez.Sum(awards => (awards.AwardPoint.Points >= 2 && awards.AwardPoint.Points < 4 ? 1 : 0)),
                    high = rez.Sum(awards => (awards.AwardPoint.Points >= 4 ? 1 : 0)),
                    total = rez.Sum(awards => awards.AwardPoint.Points)
                };

            int thisYearsTotalPoints = (from awards in db.ScoreBoardAwardsBAwards
                                        where (awards.AwardTime.Year == DateTime.Now.Year)
                                        select awards.AwardPoint).Count();

            var result = new Tuple<IEnumerable<ResultItems>, int>(scoreboardawardsbawards.ToList(), thisYearsTotalPoints);

            return View(result);
        }
        
        public ActionResult Monthly()
        {
            var result = new Tuple<DataTable, DataTable>(new DataTable(), new DataTable());

            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}