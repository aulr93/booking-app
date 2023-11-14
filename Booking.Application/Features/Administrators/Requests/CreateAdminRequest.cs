using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Administrators.Commands;

namespace Booking.Application.Features.Administrators.Requests
{
    public class CreateAdminRequest : IMapping
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAdminRequest, CreateAdminCommand>()
                .ConstructUsing(x => new CreateAdminCommand(x.Username, x.Password));
        }
    }
}