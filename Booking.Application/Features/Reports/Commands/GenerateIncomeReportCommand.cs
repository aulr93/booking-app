using Booking.Application.Commons.Interfaces;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features.Reports.Commands
{
    public class GenerateIncomeReportCommand : IRequest<Unit>
    {
        public GenerateIncomeReportCommand(DateTime period)
        {
            Period = period;
        }

        public DateTime Period { get; }
    }

    public class GenerateIncomeReportCommandHandler : IRequestHandler<GenerateIncomeReportCommand, Unit>
    {
        private readonly IDapperContext _dapperContext;

        public GenerateIncomeReportCommandHandler(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<Unit> Handle(GenerateIncomeReportCommand request, CancellationToken cancellationToken)
        {
            var param = new DynamicParameters();
            param.Add("@period", request.Period.ToString("yyyy_MM"));

            using var conn = _dapperContext.CreateConnection();
            conn.Open();

            try
            {
                var queryRefreshMV = $"refresh materialized view report_income_@period_view;";
                
                await conn.ExecuteAsync(queryRefreshMV, param);

                return Unit.Value;
            }
            catch
            {
                var queryCreateMV = $"create materialized view report_income_@period_view as " +
                                    $"select hrb.\"BookingDate\", count(hrb.\"Id\") as \"TotalRoomBooked\", sum(hr.\"Price\") as \"TotalIncome\" " +
                                    $"from \"HotelRoomBooking\" hrb " +
                                    $"inner join \"HotelRoom\" hr on hr.\"Id\" = hrb.\"RoomId\" " +
                                    $"where extract('month' from hrb.\"BookingDate\") = '{request.Period.Month}' and extract('year' from hrb.\"BookingDate\") = '{request.Period.Year}' " +
                                    $"with data;";

                await conn.ExecuteAsync(queryCreateMV, param);

                return Unit.Value;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
