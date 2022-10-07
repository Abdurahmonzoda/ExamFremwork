using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ChallengeService:IChallengeService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public ChallengeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Response<List<GetChallengeDto>>> GetChallenges()
        {
            var challenge = _mapper.Map<List<GetChallengeDto>>(await _context.Challenges.ToListAsync());
           
            return new Response<List<GetChallengeDto>>(challenge);
        }

        //add location 
        public async Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto model)
        {
            try
            {
                var challenge = _mapper.Map<Challenge>(model);
                await _context.Challenges.AddAsync(challenge);
                await _context.SaveChangesAsync();
                model.Id = challenge.Id;
                return new Response<AddChallengeDto>(model);
            }
            catch (System.Exception ex)
            {
                return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<Challenge>> GetChallengeById(int id)
        {
            var find = _mapper.Map<Challenge>( await _context.Challenges.FindAsync(id));
            return new Response<Challenge>(find);
        }

        public async Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto challenge)
        {
            try
            {
                var find = await _context.Challenges.FindAsync(challenge.Id);
                if (find == null) return new Response<AddChallengeDto>(System.Net.HttpStatusCode.NotFound, "");
                find.Id = challenge.Id;
                find.Title=challenge.Title;
                find.Description=challenge.Description;

                await _context.SaveChangesAsync();
                return new Response<AddChallengeDto>(challenge);
            }
            catch (System.Exception ex)
            {
                return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteChallenge(int id)
        {
            try
            {
                var find = await _context.Challenges.FindAsync(id);
                if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");
                _context.Challenges.Remove(find);
                await _context.SaveChangesAsync();
                return new Response<string>("removed successfully");
            }
            catch (System.Exception ex)
            {
                return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<List<GetChallengeWithGroups>>> GetChallengeWithGroups()
        {
            try
            {
                var result = await (
                    from ch in _context.Challenges
                    select new GetChallengeWithGroups
                    {
                        Description = ch.Description,
                        Id = ch.Id,
                        Title = ch.Title,
                        Groups = (from g in _context.Groups
                                  where g.ChallangeId == ch.Id
                                  select _mapper.Map<GetGroupDto>(g)).ToList()

                    }).ToListAsync();

                return new Response<List<GetChallengeWithGroups>>(result);
            }
            catch(Exception ex)
            {
                return new Response<List<GetChallengeWithGroups>>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
        }

      
    }
}
