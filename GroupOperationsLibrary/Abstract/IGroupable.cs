using System.Collections.Generic;
using System.Linq;

namespace GroupOperationsLibrary.Abstract
{
    internal interface IGroupable<in T> where T : class
    {
        void Group(IEnumerable<string> fieldNameList);
    }
}