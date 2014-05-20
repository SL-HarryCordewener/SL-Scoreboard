using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardBugs
    {
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Bug Description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Assign points to this bug. 0 if it's not confirmed yet.")]
        [DisplayName("Bug Point Value")]
        [Range(0, 50, ErrorMessage = "Please choose an integer value between 0 and 50.")]
        public int Points { get; set; }

        [DisplayName("Related Defect")]
        public string Defect { get; set; }

        public ICollection<ScoreboardAwards> Awards { get; set; }
    }
}