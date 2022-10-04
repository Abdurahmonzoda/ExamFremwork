using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChallengeController
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }
        [HttpGet]
        public async Task<Response<List<GetChallengeDto>>> Get()
        {
            var challenge = await _challengeService.GetChallenges();
            return challenge;
        }

        [HttpGet("{id}")]
        public async Task<Response<Challenge>> Get(int id)
        {
            var challenge = await _challengeService.GetChallengeById(id);
            return challenge;
        }

        [HttpPost]
        public async Task<Response<AddChallengeDto>> Post(AddChallengeDto group)
        {
            var newChallenge = await _challengeService.AddChallenge(group);
            return newChallenge;
        }

        [HttpPut]
        public async Task<Response<AddChallengeDto>> Put(AddChallengeDto group)
        {
            var updatedChallenge = await _challengeService.UpdateChallenge(group);
            return updatedChallenge;
        }

        [HttpDelete]
        public async Task<Response<string>> Delete(int id)
        {
            var challenge = await _challengeService.DeleteChallenge(id);
            return challenge;
        }
    }
}
