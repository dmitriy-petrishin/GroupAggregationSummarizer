using GroupOperationsLibrary.Abstract;

namespace GroupOperationsLibrary.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using GroupOperationsLibrary.Abstract;

    public class Data : DataAssigner
    {
        public Data()
        {
        }

        public Data(
            IGrouping<object, Data> group,
            IEnumerable<string> groupList,
            IEnumerable<string> averageList = null,
            IEnumerable<string> sumList = null)
            : base(group, groupList, averageList, sumList)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}