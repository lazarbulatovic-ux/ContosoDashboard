using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoDashboard.Models
{
    public class DocumentShare
    {
        [Key]
        public int DocumentShareId { get; set; }

        public int DocumentId { get; set; }

        public int? GranteeUserId { get; set; }

        public int? GranteeTeamId { get; set; }

        public int GrantedById { get; set; }

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    }
}
