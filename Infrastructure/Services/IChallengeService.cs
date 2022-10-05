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
    public interface IChallengeService
    {
        Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto location);
        Task<Response<string>> DeleteChallenge(int id);
        Task<Response<Challenge>> GetChallengeById(int id);
        Task<Response<List<GetChallengeDto>>> GetChallenges();
        Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto group);

    }
}
