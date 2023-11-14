using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Features.Authorizations.Models;
using Booking.Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace Booking.Application.Features.Administrators.Commands
{
    public class AdminLoginCommand : IRequest<LoginVM>
    {
        public AdminLoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }

    public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, LoginVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMessageLanguageService _messageLanguage;

        public AdminLoginCommandHandler(IApplicationDbContext dbContext,
            IMessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<LoginVM> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var admin = await _dbContext.Administrators.Where(x => x.Username == request.Username).SingleOrDefaultAsync();
                if (admin is null)
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.UsernameNotFound));

                if (admin.HashedPassword != (request.Password + Encoding.Default.GetString(admin.Salt)).ToSHA512())
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.WrongPassword));

                return new LoginVM
                {
                    Id = admin.Id,
                    Username = admin.Username,
                    Name = admin.Username,
                    Role = Role.ADM
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
