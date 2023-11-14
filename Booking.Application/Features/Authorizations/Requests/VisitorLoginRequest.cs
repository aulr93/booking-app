using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Authorizations.Commands;

namespace Booking.Application.Features.Authorizations.Requests
{
    public class VisitorLoginRequest : IMapping
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VisitorLoginRequest, VisitorLoginCommand>()
                .ConstructUsing(x => new VisitorLoginCommand(x.Username, x.Password));
        }
    }
}
