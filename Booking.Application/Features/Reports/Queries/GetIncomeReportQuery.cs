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
        var reportName = $"report_income_{request.Period.ToString("yyyy_MM")}_view";

        var param = new DynamicParameters();
        param.Add("@report", $"\"report_income_{request.Period.ToString("yyyy_MM")}_view\"");

        using var conn = _dapperContext.CreateConnection();
        conn.Open();

        try
        {
            var query = $"select \"BookingDate\", \"TotalRoomBooked\", \"TotalIncome\" " +
                        $"from {reportName} " +
                        $"order by \"BookingDate\" asc;";

            var result = await conn.QueryAsync<GetIncomeReportVM>(query, param);

            return result ?? new List<GetIncomeReportVM>();
        }
        catch (Exception ex)
        {
            if (ex.Message.ToLower().Contains("does not exist"))
                throw new Exception("Laporan belum di buat.");

            throw new Exception(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }
}
