using Network.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Network.Models.Communication
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
        
        [Required]
        public Dialogue RelatedDialog { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
    }
}