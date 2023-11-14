using Booking.Application.Commons.Interfaces;
using Booking.Domain.Constant;
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
            var reportName = $"report_income_{request.Period.ToString("yyyy_MM")}_view";

            var param = new DynamicParameters();
            param.Add("@report", reportName);

            using var conn = _dapperContext.CreateConnection();
            conn.Open();

            try
            {
                var queryRefreshMV = $"refresh materialized view {reportName};";

                await conn.ExecuteAsync(queryRefreshMV, param);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                var queryCreateMV = $"create materialized view {reportName} as " +
                                    $"select hrb.\"BookingDate\"::date, count(hrb.\"Id\") as \"TotalRoomBooked\", sum(hr.\"Price\") as \"TotalIncome\" " +
                                    $"from \"{AppTable.HotelRoomBooking}\" hrb " +
                                    $"inner join \"{AppTable.HotelRoom}\" hr on hr.\"Id\" = hrb.\"RoomId\" " +
                                    $"where extract('month' from hrb.\"BookingDate\") = '{request.Period.Month}' and extract('year' from hrb.\"BookingDate\") = '{request.Period.Year}' " +
                                    $"group by hrb.\"BookingDate\"::date " +
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
