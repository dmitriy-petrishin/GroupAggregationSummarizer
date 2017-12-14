namespace GroupOperationsLibrary.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class DataAssigner
    {
        protected DataAssigner() { }
        protected DataAssigner(
            IGrouping<object, DataAssigner> group,
            IEnumerable<string> groupList,
            IEnumerable<string> averageList = null,
            IEnumerable<string> sumList = null)
        {
            this.Initialize(group, groupList, averageList, sumList);
        }

        private dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        private dynamic GetNameFromGroup(IGrouping<object, DataAssigner> group, int count)
        {
            var result = this.Cast(group.Key, group.Key.GetType());

            return GetPropertyValue(result, $"Item{count}");
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }

        private void Initialize(
            IGrouping<object, DataAssigner> group,
            IEnumerable<string> groupList = null,
            IEnumerable<string> averageList = null,
            IEnumerable<string> sumList = null)
        {
            this.SetGroups(group, groupList);
            this.SetAverages(group, averageList);
            this.SetSums(group, sumList);
        }

        private void SetAverages(IGrouping<object, DataAssigner> group, IEnumerable<string> averageList)
        {
            if (averageList == null)
            {
                return;
            }

            foreach (var item in averageList)
            {
                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, group.Average(avr => (double)this.GetPropertyValue(avr, item)));
                }
            }
        }

        private void SetGroups(IGrouping<object, DataAssigner> group, IEnumerable<string> groupList)
        {
            if (groupList == null)
            {
                return;
            }

            var groupListArray = groupList.ToArray();

            for (int i = 0; i < groupListArray.Length; i++)
            {
                var item = groupListArray[i];

                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, this.GetNameFromGroup(group, i + 1));
                }
            }
        }

        private void SetSums(IGrouping<object, DataAssigner> group, IEnumerable<string> sumList)
        {
            if (sumList == null)
            {
                return;
            }

            foreach (var item in sumList)
            {
                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, group.Sum(avr => (int)this.GetPropertyValue(avr, item)));
                }
            }
        }
    }
}