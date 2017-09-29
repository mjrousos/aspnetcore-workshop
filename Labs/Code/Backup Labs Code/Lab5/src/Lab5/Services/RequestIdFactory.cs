using System.Threading;

namespace Lab5.Services
{
    public interface IRequestIdFactory
    {
        string MakeRequestId();
    }

    public class RequestIdFactory : IRequestIdFactory
    {
        private int _requestId;

        public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
    }
}