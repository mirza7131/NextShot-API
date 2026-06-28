using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace AppCommonMethods
{
    public static class ExtensionMethods
    {
        public static bool isDailyMethodInUse = false;
        public static bool isDailyWithdrawalMethodInUse = false;
        public static bool isDailyRevertInactiveDisableMethodInUse = false;
        public static bool isDailyInvalidForQpMethodInUse = false;
        public static bool isDailyInvalidForExpiryMethodInUse = false;

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                var myClassType = Type.GetType(String.Format("{0}.{1}", property.PropertyType.Namespace, property.PropertyType.Name));
                var type = property.DeclaringType.BaseType.FullName;
                if (property.PropertyType.Namespace.ToString() != "System.Collections.Generic")
                {
                    if (property.PropertyType == typeof(System.DayOfWeek))
                    {
                        DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                        property.SetValue(item, day, null);
                    }

                    else
                    {
                        if (row[property.Name] == DBNull.Value)
                            property.SetValue(item, null, null);
                        else
                            property.SetValue(item, row[property.Name], null);
                    }
                }
            }
            return item;
        }

        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}
