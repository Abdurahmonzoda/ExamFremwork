using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GroupService:IGroupService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public GroupService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<GetGroupDto>>> GetGroups()
        {
            var group =  _mapper.Map<List<GetGroupDto>>(await _context.Groups.ToListAsync());
            return new Response<List<GetGroupDto>>(group);
        }

        //add location 
        public async Task<Response<AddGroupDto>> AddGroup(AddGroupDto model)
        {
            try
            {
                var group = _mapper.Map<Group>(model);
                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();
                model.Id = group.Id;
                return new Response<AddGroupDto>(model);
            }
            catch (System.Exception ex)
            {
                return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<Group>> GetGroupById(int id)
        {
            var find = await _context.Groups.FindAsync(id);
            return new Response<Group>(find);
        }

        public async Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group)
        {
            try
            {
                var find = await _context.Groups.FindAsync(group.Id);
                if (find == null) return new Response<AddGroupDto>(System.Net.HttpStatusCode.NotFound, "");
                find.Id = group.Id;
                find.GroupNick = group.GroupNick;
                find.ChallangeId = group.ChallangeId;
                find.NeededMember = group.NeededMember;
                find.TeamSlogan = group.TeamSlogan;

                await _context.SaveChangesAsync();
                return new Response<AddGroupDto>(group);
            }
            catch (System.Exception ex)
            {
                return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteGroup(int id)
        {
            try
            {
                var find = await _context.Groups.FindAsync(id);
                if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");
                _context.Groups.Remove(find);
                await _context.SaveChangesAsync();
                return new Response<string>("removed successfully");
            }
            catch (System.Exception ex)
            {
                return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<List<GetGtoupWithChallengeNameDto>>> GetGroupWithChallengeName()
        {
            try
            {
                var group = await (from gr in _context.Groups
                                   join ch in _context.Challenges
                                   on gr.ChallangeId equals ch.Id
                                   select new GetGtoupWithChallengeNameDto
                                   {
                                       ChallengeId = ch.Id,
                                       ChallengeName = ch.Title,
                                       GroupNick = gr.GroupNick,
                                       NeededMember = gr.NeededMember,
                                       TeamSlogan = gr.TeamSlogan, 
                                       Id = ch.Id

                                   }).ToListAsync();
                return new Response<List<GetGtoupWithChallengeNameDto>>(group); 
            }
            catch(Exception ex)
            {
                return new Response<List<GetGtoupWithChallengeNameDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public async Task<Response<List<GetGroupWithParticipants>>> GetGroupWithParticipants()
        {
            try
            {
                var participants = await (from gr in _context.Groups
                                          select new GetGroupWithParticipants()
                                          {
                                              Id = gr.Id,
                                              GroupNick = gr.GroupNick,
                                              ChallangeId = gr.ChallangeId,
                                              NeededMember = gr.NeededMember,
                                              TeamSlogan = gr.TeamSlogan,
                                              Participants = (from pr in _context.Participants
                                                              where gr.Id == pr.GroupId
                                                              select _mapper.Map<GetParticipantDto>(pr)).ToList()
                                          }).ToListAsync();
                return new Response<List<GetGroupWithParticipants>>(participants);
            }
            catch(Exception ex)
            {
                return new Response<List<GetGroupWithParticipants>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
