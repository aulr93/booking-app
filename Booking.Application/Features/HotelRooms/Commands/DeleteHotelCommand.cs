﻿using Booking.Application.Commons.Constants;
using Booking.Application.Commons.Helpers;
using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Models;
using Booking.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booking.Application.Features.HotelRooms.Commands
{
    public class DeleteHotelCommand : IRequest<List<DeleteVM>>
    {
        public List<Guid> Ids { get; set; }

        public DeleteHotelCommand(List<Guid> ids)
        {
            Ids = ids;
        }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, List<DeleteVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly MessageLanguage _messageLanguage;

        public DeleteHotelCommandHandler(IApplicationDbContext dbContext,
            MessageLanguage messageLanguage)
        {
            _dbContext = dbContext;
            _messageLanguage = messageLanguage;
        }

        public async Task<List<DeleteVM>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var result = new List<DeleteVM>();
            var hotelRooms = await _dbContext.hotelRooms.Where(x => request.Ids.Any(id => id == x.Id)).ToListAsync();

            foreach (var id in request.Ids)
            {
                var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
                try
                {
                    var data = hotelRooms.FirstOrDefault(x => x.Id == id);
                    if (data == null)
                    {
                        result.Add(new DeleteVM { Id = id, IsSuccess = false, Message = _messageLanguage[MessageCodeConstant.DataNotFound] });
                        continue;
                    }

                    _dbContext.hotelRooms.Remove(data);

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    result.Add(new DeleteVM { Id = id, IsSuccess = true });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    result.Add(new DeleteVM { Id = id, IsSuccess = false, Message = ex.Message });
                }
            }

            return result;
        }
    }
}