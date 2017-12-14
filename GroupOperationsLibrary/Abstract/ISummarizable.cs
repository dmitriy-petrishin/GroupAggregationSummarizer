using System.Collections.Generic;
using System.Linq;

namespace GroupOperationsLibrary.Abstract
{
    internal interface ISummarizable<in T> where T : class
    {
        void Summarize(IEnumerable<string> fieldNameList);
    }
}