using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupOperationsLibrary.Abstract
{
    public abstract class GroupOperationsBase : 
        IGroupable<GroupOperationsBase>,
        IAverageble<GroupOperationsBase>,
        ISummarizable<GroupOperationsBase>
    {
        private IGrouping<object, GroupOperationsBase> _previouslyGroupedList;

        protected GroupOperationsBase() { }
        
        protected GroupOperationsBase(
            IGrouping<object, GroupOperationsBase> previouslyGroupedList,
            IEnumerable<string> groupFieldNameList,
            IEnumerable<string> averageFieldNameList = null,
            IEnumerable<string> sumFieldNameList = null)
        {
            this._previouslyGroupedList = previouslyGroupedList;
            this.Group(groupFieldNameList);
            this.Average(averageFieldNameList);
            this.Summarize(sumFieldNameList);
        }

        public void Group(IEnumerable<string> fieldNameList)
        {
            if (fieldNameList == null)
            {
                return;
            }

            string[] groupListArray = fieldNameList.ToArray();

            for (int i = 0; i < groupListArray.Length; i++)
            {
                var item = groupListArray[i];

                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, this.GetNameFromGroup(_previouslyGroupedList, i + 1));
                }
            }
        }

        public void Average(IEnumerable<string> fieldNameList)
        {
            if (fieldNameList == null)
            {
                return;
            }

            foreach (var item in fieldNameList)
            {
                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, _previouslyGroupedList.Average(avr => (double)GetPropertyValue(avr, item)));
                }
            }
        }

        public void Summarize(IEnumerable<string> fieldNameList)
        {
            if (fieldNameList == null)
            {
                return;
            }

            foreach (var item in fieldNameList)
            {
                PropertyInfo property = this.GetType().GetProperty(item);

                if (property != null)
                {
                    property.SetValue(this, _previouslyGroupedList.Sum(avr => (int)GetPropertyValue(avr, item)));
                }
            }
        }
        
        private dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        private dynamic GetNameFromGroup(IGrouping<object, GroupOperationsBase> group, int count)
        {
            var result = this.Cast(group.Key, group.Key.GetType());

            return GetPropertyValue(result, $"Item{count}");
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }
        
        ~GroupOperationsBase()
        {
            this._previouslyGroupedList = null;
        }
    }
}