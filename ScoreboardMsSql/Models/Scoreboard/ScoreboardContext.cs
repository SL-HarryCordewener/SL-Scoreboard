using System.Data.Entity;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardContext: DbContext
    {
        public ScoreboardContext() : base("ScoreboardContext")
        {
        }

        public DbSet<ScoreboardUsers> ScoreBoardUsers { get; set; }

        public DbSet<ScoreboardBugs> ScoreBoardBugs { get; set; }

        public DbSet<ScoreboardAwards> ScoreBoardAwardsBAwards { get; set; }
    }
}