using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ScoreboardMsSql.Models.Scoreboard;

namespace ScoreboardMsSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScoreboardContext db = new ScoreboardContext();

        public ActionResult Index()
        {
            var thisyearTotalRanking = from awards in db.ScoreBoardAwardsBAwards
                where (awards.AwardTime.Year == DateTime.Now.Year)
                group awards by new {awards.AwardUser.Id, awards.AwardUser.Name, awards.AwardPoint.Points}
                into rez
                select new
                {
                    rez.Key.Name,
                    low = rez.Sum(awards => (awards.AwardPoint.Points >= 0 && awards.AwardPoint.Points < 2 ? 1 : 0)),
                    mid = rez.Sum(awards => (awards.AwardPoint.Points >= 2 && awards.AwardPoint.Points < 4 ? 1 : 0)),
                    high = rez.Sum(awards => (awards.AwardPoint.Points >= 4 ? 1 : 0)),
                    sum = rez.Sum(awards => awards.AwardPoint.Points)
                };

            thisyearTotalRanking = thisyearTotalRanking.OrderByDescending(x => x.sum);

            var thisYearsTotalRankingTable = new DataTable();
            thisYearsTotalRankingTable.Columns.Add("POS.", typeof (int));
            thisYearsTotalRankingTable.Columns.Add("NAME", typeof (string));
            thisYearsTotalRankingTable.Columns.Add("LOW", typeof (string));
            thisYearsTotalRankingTable.Columns.Add("MID", typeof (string));
            thisYearsTotalRankingTable.Columns.Add("HIGH", typeof (string));
            thisYearsTotalRankingTable.Columns.Add("Total Points", typeof (int));

            int pos = 1;
            foreach (var item in thisyearTotalRanking)
            {
                DataRow row = thisYearsTotalRankingTable.NewRow();
                row["POS."] = pos;
                row["Name"] = item.Name;
                row["LOW"] = item.low;
                row["MID"] = item.mid;
                row["HIGH"] = item.high;
                row["Total Points"] = item.sum;
                thisYearsTotalRankingTable.Rows.Add(row);
                pos++;
            }

            int thisYearsTotalPoints = (from awards in db.ScoreBoardAwardsBAwards
                where (awards.AwardTime.Year == DateTime.Now.Year)
                select awards.AwardPoint).Count();

            var result = new Tuple<DataTable, int>(thisYearsTotalRankingTable, thisYearsTotalPoints);

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