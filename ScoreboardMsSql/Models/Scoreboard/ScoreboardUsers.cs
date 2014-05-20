using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreboardMsSql.Models.Scoreboard
{
    public class ScoreboardUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Field is required")]
        [DisplayName("User's Name or Initials")]
        [MaxLength(40)]
        public string Name { get; set; }

        public virtual ICollection<ScoreboardAwards> Awards { get; set; }
    }
}