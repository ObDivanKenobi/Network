using Network.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Network.Models.Communication
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        public string Tags { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Posted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public List<Comment> OrderedByDateComments
        {
            get { return Comments.OrderBy(c => c.Date).ToList(); }
        }
    }
}