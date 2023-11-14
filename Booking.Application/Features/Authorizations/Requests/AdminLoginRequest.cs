using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Administrators.Commands;

namespace Booking.Application.Features.Authorizations.Requests
{
    public class AdminLoginRequest : IMapping
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AdminLoginRequest, AdminLoginCommand>()
                .ConstructUsing(x => new AdminLoginCommand(x.Username, x.Password));
        }
    }
}
