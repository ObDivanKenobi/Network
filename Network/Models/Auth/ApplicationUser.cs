using Microsoft.AspNet.Identity.EntityFramework;
using Network.Models.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Network.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ProfessionalArea> Areas { get; set; }
        public virtual ICollection<ApplicationUser> Colleagues { get; set; }
        [Required]
        public bool PostsForColleaguesOnly { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<FriendshipRequest> OutgoingFriendshipRequests { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<FriendshipRequest> IncomingFriendshipRequests { get; set; }
        [Required]
        public uint UnreadMessagesCount { get; set; }
    }
}