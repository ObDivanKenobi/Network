using Network.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Network.Models.Communication
{
    public class Dialogue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ICollection<UserInDialog> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateDate { get; set; }

        [NotMapped]
        public List<Message> OrderedByDateMessages
        {
            get { return Messages.OrderBy(m => m.Date).ToList(); }
        }
    }
}