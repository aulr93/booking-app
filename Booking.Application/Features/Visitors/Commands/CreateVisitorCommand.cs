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

namespace Booking.Application.Features.Visitors.Commands
{
    public class CreateVisitorCommand : IRequest<Unit>
    {
        public CreateVisitorCommand(string username, string name, string nik, string email, string password)
        {
            Username = username;
            Name = name;
            Nik = nik;
            Email = email;
            Password = password;
        }

        public string Username { get; }
        public string Name { get; }
        public string Nik { get; }
        public string Email { get; }
        public string Password { get; }
    }

    public class CreateVisitorCommandHandler : IRequestHandler<CreateVisitorCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly MessageLanguageService _messageLanguage;

        public CreateVisitorCommandHandler(IApplicationDbContext dbContext,
            MessageLanguageService messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<Unit> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var isExist = await _dbContext.Visitors.AnyAsync(x => x.Username == request.Username || x.Email == request.Email || x.NIK == request.Nik);
                if (isExist)
                    throw new BadRequestException(_messageLanguage[MessageCodeConstant.DataExist]);

                var visitorID = Guid.NewGuid();
                var salt = SaltGenerator.GetSalt();
                var password = AuthorizationHelper.GetRandomPassword();

                _dbContext.Visitors.Add(new Visitor
                {
                    Id = visitorID,
                    Username = request.Username,
                    Salt = Encoding.Default.GetBytes(salt),
                    HashedPassword = (password + salt).ToSHA512(),
                    Name = request.Name,
                    NIK = request.Nik,
                    Email = request.Email
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
