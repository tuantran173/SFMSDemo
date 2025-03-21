using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Reports
{
    public interface IReportService
    {
        Task<object> GetGeneralReportAsync();
        Task<object> GetFacilityReportAsync(Guid facilityId);
        Task<List<object>> GetTopBookedFacilitiesAsync();
    }
}
