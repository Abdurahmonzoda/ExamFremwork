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
    public interface ILocationService
    {
        Task<Response<AddLocationDto>> AddLocation(AddLocationDto location);
        Task<Response<string>> DeleteLocation(int id);
        Task<Response<Location>> GetLocationById(int id);
        Task<Response<List<GetLocationDto>>> GetLocations();
        Task<Response<AddLocationDto>> UpdateLocation(AddLocationDto location);
    }
}
