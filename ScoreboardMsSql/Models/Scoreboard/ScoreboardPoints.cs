using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardPoints
    {
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Point Type Description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Assign points to this Point Type. 0 if it's not confirmed yet.")]
        [DisplayName("Point Type Value")]
        [Range(0, 50, ErrorMessage = "Please choose an integer value between 0 and 50.")]
        public int Points { get; set; }

        // TO BE PHASED OUT!!
        //[DisplayName("Related Defect")]
        //public string Defect { get; set; }

        public ICollection<ScoreboardAwards> Awards { get; set; }
    }
}