using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class GetGtoupWithChallengeNameDto
    {
        public int Id { get; set; }
        public string GroupNick { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeName { get; set; }
        public bool NeededMember { get; set; }
        public string TeamSlogan { get; set; }
    }
}
