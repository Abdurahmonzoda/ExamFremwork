using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string GroupNick { get; set; }
        public int ChallangeId { get; set; }
        public virtual Challenge Challange { get; set; }
        public bool NeededMember { get; set; }
        [MaxLength(300)]
        public string TeamSlogan { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<Participant> Participants { get; set; }

        public Group()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
