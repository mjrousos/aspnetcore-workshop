using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Services
{
    public interface IValuesService
    {
        IEnumerable<string> GetValues();
    }

    public class DefaultValuesService : IValuesService
    {
        public IEnumerable<string> GetValues() => new string[] { "value1", "value2" };
    }
}
