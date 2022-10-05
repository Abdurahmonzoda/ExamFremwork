using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        { 
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<Response<List<GetParticipantDto>>> Get()
        {
            var participant = await _participantService.GetParticipants();
            return participant;
        }
        [HttpGet("WithGropName")]
        public async Task<Response<List<GetParticipantWithGroupName>>> GetWithGroupName()
        {
            var participant = await _participantService.GetParticipantWithGroupName();
            return participant;
        }

        [HttpGet("{id}")]
        public async Task<Response<Participant>> Get(int id)
        {
            var participant = await _participantService.GetParticipantById(id);
            return participant;
        }

        [HttpPost]
        public async Task<Response<AddParticipantDto>> Post(AddParticipantDto participant)
        {
            var newParticipant = await _participantService.AddParticipant(participant);
            return newParticipant;
        }

        [HttpPut]
        public async Task<Response<AddParticipantDto>> Put(AddParticipantDto participant)
        {
            var updatedParticipant = await _participantService.UpdateLocation(participant);
            return updatedParticipant;
        }

        [HttpDelete]
        public async Task<Response<string>> Delete(int id)
        {
            var participant = await _participantService.DeleteParticipant(id);
            return participant;
        }
    }
}
