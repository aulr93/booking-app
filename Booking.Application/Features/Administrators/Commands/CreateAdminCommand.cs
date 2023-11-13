using Booking.Application.Common.Exceptions;
using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Helpers;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Services;
using Booking.Common;
using Booking.Common.Extensions;
using Booking.Domain.Entities.Masters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Booking.Application.Features.Administrators.Commands
{
    public class CreateAdminCommand : IRequest<Unit>
    {
        public CreateAdminCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }

    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMessageLanguageService _messageLanguage;

        public CreateAdminCommandHandler(IApplicationDbContext dbContext,
            IMessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var isExist = await _dbContext.Administrators.AnyAsync(x => x.Username == request.Username);
                if (isExist)
                    throw new BadRequestException(_messageLanguage.GetLabels(MessageCodeConstant.DataExist));

                var adminID = Guid.NewGuid();
                var salt = SaltGenerator.GetSalt();
                var password = request.Password;

                _dbContext.Administrators.Add(new Administrator
                {
                    Id = adminID,
                    Username = request.Username,
                    Salt = Encoding.Default.GetBytes(salt),
                    HashedPassword = (password + salt).ToSHA512(),
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception(ex.Message);
            }
        }
    }
}
