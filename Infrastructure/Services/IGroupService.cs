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
    public interface IGroupService
    {
        Task<Response<AddGroupDto>> AddGroup(AddGroupDto location);
        Task<Response<string>> DeleteGroup(int id);
        Task<Response<Group>> GetGroupById(int id);
        Task<Response<List<GetGroupDto>>> GetGroups();

        Task<Response<List<GetGtoupWithChallengeNameDto>>> GetGroupWithChallengeName();
        Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group);
    }
}
