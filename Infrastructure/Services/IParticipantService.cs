using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IParticipantService
    {
        Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto location);
        Task<Response<string>> DeleteParticipant(int id);
        Task<Response<Participant>> GetParticipantById(int id);
        Task<Response<List<GetParticipantDto>>> GetParticipants();
        Task<Response<AddParticipantDto>> UpdateLocation(AddParticipantDto location);
        Task<Response<List<GetParticipantWithGroupName>>> GetParticipantWithGroupName(); 
    }
}
