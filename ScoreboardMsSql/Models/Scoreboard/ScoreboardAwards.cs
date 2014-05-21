using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardAwards
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must award this to a user.")]
        [DisplayName("Awarded User")]
        public virtual ScoreboardUsers AwardUser { get; set; }

        [Required(ErrorMessage = "You must assign a bug for this award.")]
        [DisplayName("Awarded Point")]
        public virtual ScoreboardPoints AwardPoint { get; set; }

        [Required(ErrorMessage = "At what time was this bug assigned?")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Awarded At")]
        public DateTime AwardTime { get; set; }
    }
}