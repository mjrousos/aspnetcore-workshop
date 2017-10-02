using System.Threading;

namespace AspNetCoreWorkshop.Services
{
    public class RequestCounter : IRequestIdFactory
    {
        private int _requestId;

        public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
    }
}
