namespace GroupOperationsLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Constants;
    using Abstract;

    public class GroupOperationsProvider<T>
        where T : GroupOperationsBase
    {
        /// <summary>
        /// Recieves list of the objects to grouping, averaging, summation and return transformated list with the same object type.
        /// </summary>
        /// <param name="list">List to transformation.</param>
        /// <param name="groupByList">List of fields names to grouping.</param>
        /// <param name="averageList">List of fields names to averaging.</param>
        /// <param name="sumList">List of fields names to summation.</param>
        /// <returns> Transformed list.</returns>
        public IEnumerable<T> Execute(
            IEnumerable<T> list,
            IEnumerable<string> groupByList,
            IEnumerable<string> averageList = null,
            IEnumerable<string> sumList = null)
        {
            Expression<Func<T, object>> expression = this.GroupByExpression<T>(groupByList.ToArray());

            IEnumerable<IGrouping<object, T>> groupedList = list.GroupBy(expression.Compile());

            return groupedList.Select(group => (T)Activator.CreateInstance(typeof(T), group, groupByList, averageList, sumList));
        }

        private Expression<Func<T, object>> GroupByExpression<T>(string[] propertyNames)
        {
            PropertyInfo[] properties = propertyNames.Select(name => typeof(T).GetProperty(name)).ToArray();
            Type[] propertyTypes;
            try
            {
                propertyTypes = properties.Select(p => p.PropertyType).ToArray();
            }
            catch (Exception ex)
            {
                // logging +
                throw new NullReferenceException(Messages.PropertyNotFound, ex);
            }

            Type tupleTypeDefinition = typeof(Tuple).Assembly.GetType("System.Tuple`" + properties.Length);
            Type tupleType = tupleTypeDefinition.MakeGenericType(propertyTypes);
            ConstructorInfo constructor = tupleType.GetConstructor(propertyTypes);
            if (constructor == null)
            {
                // logging +
                throw new InvalidOperationException();
            }

            ParameterExpression param = Expression.Parameter(typeof(T), "item");
            Expression body = Expression.New(constructor, properties.Select(p => Expression.Property(param, p)));
            Expression<Func<T, object>> expr = Expression.Lambda<Func<T, object>>(body, param);
            return expr;
        }
    }
}