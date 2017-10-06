using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Services
{
    public class RequestCounter : IRequestIdFactory
    {
        private int _requestId;

        public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
    }
}
