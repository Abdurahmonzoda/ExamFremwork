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
    public class ChallengeService:IChallengeService
    {
        private readonly DataContext _context;

        public ChallengeService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<List<GetChallengeDto>>> GetChallenges()
        {
            var challenge = await _context.Challenges.Select(p => new GetChallengeDto()
            {
               Id = p.Id,
               Title = p.Title,
               Description = p.Description

            }).ToListAsync();
            return new Response<List<GetChallengeDto>>(challenge);
        }

        //add location 
        public async Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto model)
        {
            try
            {
                var challenge = new Challenge()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description
                };
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
            var find = await _context.Challenges.FindAsync(id);
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

        
    }
}
