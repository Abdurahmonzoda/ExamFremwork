using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class InfrastructureProfile: Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Challenge, GetChallengeDto>().ReverseMap();
            CreateMap<Group, GetGroupDto>().ReverseMap();
            CreateMap<Location, GetLocationDto>().ReverseMap();
            CreateMap<Participant, GetParticipantDto>().ReverseMap();

            CreateMap<Challenge, AddChallengeDto>().ReverseMap();
            CreateMap<Group, AddGroupDto>().ReverseMap();
            CreateMap<Location, AddLocationDto>().ReverseMap();
            CreateMap<Participant, AddParticipantDto>().ReverseMap();

        }
    }
}
