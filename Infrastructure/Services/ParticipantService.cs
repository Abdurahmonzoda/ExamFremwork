using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly DataContext _context;

        public ParticipantService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<List<GetParticipantDto>>> GetParticipants()
        {
            var participant = await _context.Participants.Select(p => new GetParticipantDto()
            {
                Id = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                Phone = p.Phone,
                GroupId = p.GroupId,
                LocationId = p.LocationId

            }).ToListAsync();
            return new Response<List<GetParticipantDto>>(participant);
        }

        //add location 
        public async Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto model)
        {
            try
            {
                var participant = new Participant()
                {
                    Id = model.Id,
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone,
                    GroupId = model.GroupId,
                    LocationId = model.LocationId
                };
                await _context.Participants.AddAsync(participant);
                await _context.SaveChangesAsync();
                model.Id = participant.Id;
                return new Response<AddParticipantDto>(model);
            }
            catch (System.Exception ex)
            {
                return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<Participant>> GetParticipantById(int id)
        {
            var find = await _context.Participants.FindAsync(id);
            return new Response<Participant>(find);
        }

        public async Task<Response<AddParticipantDto>> UpdateLocation(AddParticipantDto location)
        {
            try
            {
                var find = await _context.Participants.FindAsync(location.Id);
                if (find == null) return new Response<AddParticipantDto>(System.Net.HttpStatusCode.NotFound, "");

                // if location is found
                find.Id = location.Id;
                find.FullName = location.FullName;
                find.Email = location.Email;
                find.Phone = location.Phone;
                find.GroupId = location.GroupId;
                find.LocationId = location.LocationId;
                await _context.SaveChangesAsync();
                return new Response<AddParticipantDto>(location);
            }
            catch (System.Exception ex)
            {
                return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteParticipant(int id)
        {
            try
            {
                var find = await _context.Participants.FindAsync(id);
                if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");

                _context.Participants.Remove(find);
                await _context.SaveChangesAsync();
                return new Response<string>("removed successfully");
            }
            catch (System.Exception ex)
            {
                return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<List<GetParticipantWithGroupName>>> GetParticipantWithGroupName()
        {
            try
            {
                var participant = await (from pr in _context.Participants
                                         join gr in _context.Groups
                                         on pr.GroupId equals gr.Id
                                         join lo in _context.Locations
                                         on pr.LocationId equals lo.Id
                                         select new GetParticipantWithGroupName
                                         {
                                             Id = pr.Id,
                                             FullName = pr.FullName,
                                             Email = pr.Email, 
                                             Phone = pr.Phone,
                                             CreatedAt = pr.CreatedAt,
                                             GroupId = pr.GroupId,
                                             GroupName = gr.GroupNick,
                                             LocationId = pr.LocationId,
                                             LocationName = lo.Title 

                                         }).ToListAsync();
                return new Response<List<GetParticipantWithGroupName>>(participant);
            }
            catch (Exception ex)
            {
                return new Response<List<GetParticipantWithGroupName>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
