using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{
    /// <summary>
    ///     Standard controller that controls Views/Home/*
    /// </summary>
    public class HomeController : Controller
    {
        private const int LowMax = 5; // Range for Low level points: 0-LowMax
        private const int MidMax = 10; // Range for Mid Level Points: LowMax-MidMax
        private readonly ScoreboardContext _db = new ScoreboardContext(); // The Model that contains our information.

        // We need this to make it easier to list the index. 
        // LINQ Anonymous class lists do not play nice.

        /// <summary>
        ///     Views/Home/Index
        ///     Views/Home/
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult Index()
        {
            // var scoreboardawardsbawards = db.ScoreBoardAwardsBAwards.Include(award => award.AwardPoint).Include(award => award.AwardUser);
            IQueryable<ResultItems> scoreboardawardsbawards =
                from awards in _db.ScoreBoardAwardsBAwards
                where (awards.AwardTime.Year == DateTime.Now.Year && awards.AwardTime.Month == DateTime.Now.Month)
                group awards by new {awards.AwardUser.Id, awards.AwardUser.Name, awards.AwardPoint.Points}
                into rez
                select new ResultItems
                {
                    Name = rez.Key.Name,
                    Low =
                        rez.Sum(awards => (awards.AwardPoint.Points >= 0 && awards.AwardPoint.Points < LowMax ? 1 : 0)),
                    Mid =
                        rez.Sum(
                            awards => (awards.AwardPoint.Points >= LowMax && awards.AwardPoint.Points < MidMax ? 1 : 0)),
                    High = rez.Sum(awards => (awards.AwardPoint.Points >= MidMax ? 1 : 0)),
                    Total = rez.Sum(awards => awards.AwardPoint.Points)
                };

            var thisYearsTotalPoints = (from awards in _db.ScoreBoardAwardsBAwards
                where (awards.AwardTime.Year == DateTime.Now.Year && awards.AwardTime.Month == DateTime.Now.Month)
                select awards.AwardPoint).Count();

            var result = new Tuple<IEnumerable<ResultItems>, int>(scoreboardawardsbawards.ToList(), thisYearsTotalPoints);

            return View(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public class ResultItems
        {
            public int Pos;
            public string Name;
            public int High;
            public int Low;
            public int Mid;
            public int Total;
        }
    }
}