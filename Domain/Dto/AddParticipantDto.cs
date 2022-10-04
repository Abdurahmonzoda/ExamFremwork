using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class AddParticipantDto
    {

        public int Id { get; set; }
        [Required, MaxLength(60)]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(13)]
        public string Phone { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }

    }
}
