using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        [Required, MaxLength(60)]
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required, MaxLength(13)]
        public string? Phone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public Participant()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
