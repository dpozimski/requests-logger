using System;
using System.Threading.Tasks;

namespace RequestsLogger.Repository
{
    public interface IRequestsRepository
    {
        Task InsertAsync(string content, string clientIp);
    }
}
