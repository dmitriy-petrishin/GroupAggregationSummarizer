using System.Collections.Generic;
using System.Linq;

namespace GroupOperationsLibrary.Tests.Models
{
    public class ExtendedData : Data
    {
        public ExtendedData() { }

        public ExtendedData(
            IGrouping<object, Data> group,
            IEnumerable<string> groupList,
            IEnumerable<string> averageList = null,
            IEnumerable<string> sumList = null)
            : base(group, groupList, averageList, sumList) 
        {
        }

        public int StockQuantity { get; set; }
    }
}