using System.Collections.Generic;
using System.Linq;

namespace GroupOperationsLibrary.Abstract
{
    internal interface IAverageble<in T> where T : class
    {
        void Average(IEnumerable<string> fieldNameList);
    }
}