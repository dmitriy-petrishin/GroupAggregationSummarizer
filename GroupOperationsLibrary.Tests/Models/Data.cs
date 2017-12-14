using System.Collections.Generic;
using System.Linq;
using GroupOperationsLibrary.Abstract;

namespace GroupOperationsLibrary.Tests.Models
{
    public class Data : GroupOperationsBase
    {
        public Data() { }
        public Data(
            IGrouping<object, GroupOperationsBase> previouslyGroupedList, 
            IEnumerable<string> groupFieldNameList, 
            IEnumerable<string> averageFieldNameList = null, 
            IEnumerable<string> sumFieldNameList = null) 
            : base(previouslyGroupedList, groupFieldNameList, averageFieldNameList, sumFieldNameList)
        {
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }    
    }
}