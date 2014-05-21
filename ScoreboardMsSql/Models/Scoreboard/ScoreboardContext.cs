using System.Data.Entity;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardContext: DbContext
    {
        public ScoreboardContext() : base("ScoreboardContext")
        {
        }

        public DbSet<ScoreboardUsers> ScoreBoardUsers { get; set; }

        public DbSet<ScoreboardPoints> ScoreBoardPoints { get; set; }

        public DbSet<ScoreboardAwards> ScoreBoardAwardsBAwards { get; set; }
    }
}