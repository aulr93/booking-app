using AutoMapper;
using Booking.Application.Application.Mappers;
using Booking.Application.Features.Visitors.Commands;

namespace Booking.Application.Features.Visitors.Requests
{
    public class CreateVisitorRequest : IMapping
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Nik { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateVisitorRequest, CreateVisitorCommand>()
                .ConstructUsing(x => new CreateVisitorCommand(x.Username, x.Name, x.Nik, x.Email, x.Password));
        }
    }
}