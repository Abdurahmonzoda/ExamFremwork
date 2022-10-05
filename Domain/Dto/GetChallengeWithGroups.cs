using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class GetChallengeWithGroups
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GetGroupDto> Groups { get; set; }
    }
}
