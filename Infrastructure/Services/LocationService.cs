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
    public class LocationService:ILocationService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        public LocationService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        public async Task<Response<List<GetLocationDto>>> GetLocations()
        {
            var locations = _mapper.Map<List<GetLocationDto>>(await _context.Locations.ToListAsync());
            return new Response<List<GetLocationDto>>(locations);
        }

        //add location 
        public async Task<Response<AddLocationDto>> AddLocation(AddLocationDto model)
        {
            try
            {
                var location = _mapper.Map<Location>(model);
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
                model.Id = location.Id;
                return new Response<AddLocationDto>(model);
            }
            catch (System.Exception ex)
            {
                return new Response<AddLocationDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<Location>> GetLocationById(int id)
        {
            var find = await _context.Locations.FindAsync(id);
            return new Response<Location>(find);
        }

        public async Task<Response<AddLocationDto>> UpdateLocation(AddLocationDto location)
        {
            try
            {
                var find = await _context.Locations.FindAsync(location.Id);
                if (find == null) return new Response<AddLocationDto>(System.Net.HttpStatusCode.NotFound, "");

                // if location is found
                find.Description = location.Description;
                find.Title = location.Title;
                await _context.SaveChangesAsync();
                return new Response<AddLocationDto>(location);
            }
            catch (System.Exception ex)
            {
                return new Response<AddLocationDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<string>> DeleteLocation(int id)
        {
            try
            {
                var find = await _context.Locations.FindAsync(id);
                if (find == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "");

                _context.Locations.Remove(find);
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
