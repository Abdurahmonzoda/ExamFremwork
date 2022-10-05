using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class GetParticipantWithGroupName
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }

    }
}
