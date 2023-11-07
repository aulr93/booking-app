using Booking.Application.Commons.Interfaces;
using Booking.Application.Commons.Models;
using Booking.Application.Features.Reports.Models;
using Booking.Application.Features.Reports.Queries;
using Dapper;
using MediatR;

namespace Booking.Application.Features.Reports.Queries
{
    public class GetIncomeReportQuery : BasePagination, IRequest<IEnumerable<GetIncomeReportVM>>
    {
        public GetIncomeReportQuery(DateTime period)
        {
            Period = period;
        }

        public DateTime Period { get; }
    }
}

public class GetIncomeReportQueryHandler : IRequestHandler<GetIncomeReportQuery, IEnumerable<GetIncomeReportVM>>
{
    private readonly IDapperContext _dapperContext;

    public GetIncomeReportQueryHandler(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<GetIncomeReportVM>> Handle(GetIncomeReportQuery request, CancellationToken cancellationToken)
    {
        var param = new DynamicParameters();
        param.Add("@period", request.Period.ToString("yyyy_MM"));

        using var conn = _dapperContext.CreateConnection();
        conn.Open();

        try
        {
            var query = $"select \"BookingDate\", \"TotalRoomBooked\", \"TotalIncome\" " +
                        $"from report_income_@period_view " +
                        $"order by \"BookingDate\" asc;";

            var result = await conn.QueryAsync<GetIncomeReportVM>(query, param);

            return result ?? new List<GetIncomeReportVM>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }
}
