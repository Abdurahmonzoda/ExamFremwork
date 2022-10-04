using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class GetGroupDto
    {
        public int Id { get; set; }
        public string GroupNick { get; set; }
        public int ChallangeId { get; set; }
        public bool NeededMember { get; set; }
        public string TeamSlogan { get; set; }
    }
}
