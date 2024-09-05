using BAL.Requests;
using BAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ILotteryService
    {
        Task<GenerateLotteryResultResponse> GenerateLotteryResultAsync();
        Task<GetCurrentUserBetResponse> GetCurrentUserBetAsync(Guid accountId);
        Task<IEnumerable<GetUserBetsResponse>> GetUserBetsAsync(Guid accountId);
        Task<PlaceBetResponse> PlaceBetAsync(Guid accountId, PlaceBetRequest request);
    }
}
