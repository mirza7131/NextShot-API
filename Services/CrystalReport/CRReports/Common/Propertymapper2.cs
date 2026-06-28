using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace CRReports.Common
{
    public class Propertymapper2
    {
    }
    public static class PropertyMapper
    {
        #region "Public Methods"

        /// <summary>
        /// ExtensionMethod that creates a List(of Target) from the IDataReader
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="Reader">IDataReader</param>
        /// <returns>Generic List</returns>
        public static List<Target> ToList<Target>(this IDataReader Reader, Boolean MustMapAllProperties = true)
        {
            CultureInfo Culture = CultureInfo.CurrentCulture;
            return ToList<Target>(Reader, Culture, MustMapAllProperties);
        }

        /// <summary>
        /// ExtensionMethod that creates a List(of Target) from the IDataReader
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="Reader">IDataReader</param>
        /// <param name="Provider">IFormatProvider</param>
        /// <returns>Generic List</returns>
        public static List<Target> ToList<Target>(this IDataReader Reader, CultureInfo Culture, Boolean MustMapAllProperties = true)
        {
            List<Target> List = new List<Target>();
            using (Reader)
            {
                if (!Reader.IsClosed && Reader.Read())
                {
                    Func<IDataRecord, Target> Creator = ReaderMapperCache<Target>.GetCreator(Reader, Culture, MustMapAllProperties);
                    do
                    {
                        List.Add(Creator(Reader));
                    } while (Reader.Read());
                }
            }
            return List;
        }

        /// <summary>
        /// ExtensionMethod that creates a LinkedList(of Target) from the IDataReader
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="Reader">IDataReader</param>
        /// <returns>Generic List</returns>
        public static LinkedList<Target> ToLinkedList<Target>(this IDataReader Reader, Boolean MustMapAllProperties = true)
        {
            CultureInfo Culture = CultureInfo.CurrentCulture;
            return ToLinkedList<Target>(Reader, Culture, MustMapAllProperties);
        }

        /// <summary>
        /// ExtensionMethod that creates a LinkedList(of Target) from the IDataReader
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="Reader">IDataReader</param>
        /// <param name="Provider">IFormatProvider</param>
        /// <returns>Generic List</returns>
        public static LinkedList<Target> ToLinkedList<Target>(this IDataReader Reader, CultureInfo Culture, Boolean MustMapAllProperties = true)
        {
            LinkedList<Target> List = new LinkedList<Target>();
            using (Reader)
            {
                if (!Reader.IsClosed && Reader.Read())
                {
                    Func<IDataRecord, Target> Creator = ReaderMapperCache<Target>.GetCreator(Reader, Culture, MustMapAllProperties);
                    do
                    {
                        List.AddLast(Creator(Reader));
                    } while (Reader.Read());
                }
            }
            return List;
        }

        /// <summary>
        /// ExtensionMethod that creates an IEnumerable from the IDatareader
        /// </summary>
        /// <param name="Reader">IDataReader</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Target> AsEnumerable<Target>(this IDataReader Reader, Boolean MustMapAllProperties = true)
        {
            CultureInfo Culture = CultureInfo.CurrentCulture;
            using (Reader)
            {
                if (!Reader.IsClosed && Reader.Read())
                {
                    Func<IDataRecord, Target> Creator = ReaderMapperCache<Target>.GetCreator(Reader, Culture, MustMapAllProperties);
                    do
                    {
                        yield return (Creator(Reader));
                    } while (Reader.Read());
                }
            }
        }

        /// <summary>
        /// ExtensionMethod that creates an IEnumerable from the IDatareader
        /// </summary>
        /// <param name="Reader">IDataReader</param>
        /// <param name="Provider">IFormatProvider</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<Target> AsEnumerable<Target>(this IDataReader Reader, CultureInfo Culture, Boolean MustMapAllProperties = true)
        {
            using (Reader)
            {
                if (!Reader.IsClosed && Reader.Read())
                {
                    Func<IDataRecord, Target> Creator = ReaderMapperCache<Target>.GetCreator(Reader, Culture, MustMapAllProperties);
                    do
                    {
                        yield return (Creator(Reader));
                    } while (Reader.Read());
                }
            }
        }

        /// <summary>
        /// ExtensionMethod that creates a Dictionary from the IDatareader using specified key selector function
        /// </summary>
        public static Dictionary<Key, Target> ToDictionary<Key, Target>(this IDataReader Reader, Func<Target, Key> KeySelector, Boolean MustMapAllProperties = true)
        {
            return ToDictionary<Key, Target>(Reader, KeySelector, CultureInfo.CurrentCulture, MustMapAllProperties);
        }

        /// <summary>
        /// ExtensionMethod that creates a Dictionary from the IDatareader using specified key selector function
        /// </summary>
        public static Dictionary<Key, Target> ToDictionary<Key, Target>(this IDataReader Reader, Func<Target, Key> KeySelector, CultureInfo Culture, Boolean MustMapAllProperties = true)
        {
            Dictionary<Key, Target> Dict = new Dictionary<Key, Target>();
            using (Reader)
            {
                if (!Reader.IsClosed && Reader.Read())
                {
                    Func<IDataRecord, Target> Creator = ReaderMapperCache<Target>.GetCreator(Reader, Culture, MustMapAllProperties);
                    Target Item = default(Target);
                    do
                    {
                        Item = Creator(Reader);
                        Dict.Add(KeySelector(Item), Item);
                    } while (Reader.Read());
                }
            }
            return Dict;
        }

        //public static DataTable ToDataTable<T>(List<T> items)
        //{
        //   DataTable dataTable = new DataTable(typeof(T).Name);

        //   //Get all the properties
        //   PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //   foreach (PropertyInfo prop in Props)
        //   {
        //      //Defining type of data column gives proper data table 
        //      var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
        //      //Setting column names as Property names
        //      dataTable.Columns.Add(prop.Name, type);
        //   }
        //   foreach (T item in items)
        //   {
        //      var values = new object[Props.Length];
        //      for (int i = 0; i < Props.Length; i++)
        //      {
        //         //inserting property values to datatable rows
        //         values[i] = Props[i].GetValue(item, null);
        //      }
        //      dataTable.Rows.Add(values);
        //   }
        //   //put a breakpoint here and check datatable
        //   return dataTable;
        //}

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        #endregion

        #region "Private Methods"
        #region"Converter"

        /// <summary>
        /// Gets an expression representing the Source converted to the TargetType
        /// </summary>
        /// <param name="SourceType">The Type of the Source</param>
        /// <param name="SourceExpression">An Expression representing the Source value</param>
        /// <param name="TargetType">The Type of the Target</param>
        /// <returns>Expression</returns>
        private static Expression GetConversionExpression(Type SourceType, Expression SourceExpression, Type TargetType, CultureInfo Culture)
        {
            Expression TargetExpression;
            if (object.ReferenceEquals(TargetType, SourceType))
            {
                //Just assign the RecordField
                TargetExpression = SourceExpression;
            }
            else if (object.ReferenceEquals(SourceType, typeof(string)))
            {
                TargetExpression = GetParseExpression(SourceExpression, TargetType, Culture);
            }
            else if (object.ReferenceEquals(TargetType, typeof(string)))
            {
                //There are no casts from primitive types to String.
                //And Expression.Convert Method (Expression, Type, MethodInfo) only works with static methods.
                TargetExpression = Expression.Call(SourceExpression, SourceType.GetMethod("ToString", Type.EmptyTypes));
            }
            else if (object.ReferenceEquals(TargetType, typeof(bool)))
            {
                MethodInfo ToBooleanMethod = typeof(Convert).GetMethod("ToBoolean", new[] { SourceType });
                TargetExpression = Expression.Call(ToBooleanMethod, SourceExpression);
            }
            else if (object.ReferenceEquals(SourceType, typeof(Byte[])))
            {
                TargetExpression = GetArrayHandlerExpression(SourceExpression, TargetType);
            }
            else
            {
                //Using Expression.Convert works wherever you can make an explicit or implicit cast.
                //But it casts OR unboxes an object, therefore the double cast. First unbox to the SourceType and then cast to the TargetType
                //It also doesn't convert a numerical type to a String or date, this will throw an exception.
                TargetExpression = Expression.Convert(SourceExpression, TargetType);
            }
            return TargetExpression;
        }

        /// <summary>
        /// Handles conversions of Byte arrays
        /// </summary>
        /// <param name="SourceExpression"></param>
        /// <param name="TargetType"></param>
        /// <returns></returns>
        private static Expression GetArrayHandlerExpression(Expression SourceExpression, Type TargetType)
        {
            Expression TargetExpression = default(Expression);
            if (object.ReferenceEquals(TargetType, typeof(byte[])))
            {
                TargetExpression = SourceExpression;
            }
            else if (object.ReferenceEquals(TargetType, typeof(MemoryStream)))
            {
                ConstructorInfo ConstructorInfo = TargetType.GetConstructor(new[] { typeof(byte[]) });
                TargetExpression = Expression.New(ConstructorInfo, SourceExpression);
            }
            else
            {
                throw new ArgumentException("Cannot convert a byte array to " + TargetType.Name);
            }
            return TargetExpression;
        }

        /// <summary>
        /// Creates an expression that parses a string to an enum
        /// </summary>
        /// <param name="SourceExpression">The Source to parse</param>
        /// <param name="TargetType">The Type of enum</param>
        /// <returns>MethodCallExpression</returns>
        private static MethodCallExpression GetEnumParseExpression(Expression SourceExpression, Type TargetType)
        {
            //Get the MethodInfo for parsing an Enum
            MethodInfo EnumParseMethod = typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(string), typeof(bool) });
            ConstantExpression TargetMemberTypeExpression = Expression.Constant(TargetType);
            ConstantExpression IgnoreCase = Expression.Constant(true, typeof(bool));
            //Create an expression the calls the Parse method
            MethodCallExpression CallExpression = Expression.Call(EnumParseMethod, new[] { TargetMemberTypeExpression, SourceExpression, IgnoreCase });
            return CallExpression;
        }

        /// <summary>
        /// Creates an Expression that parses a string to Char or Boolean
        /// </summary>
        /// <param name="SourceExpression"></param>
        /// <param name="TargetType"></param>
        /// <returns></returns>
        private static MethodCallExpression GetGenericParseExpression(Expression SourceExpression, Type TargetType)
        {
            MethodInfo ParseMetod = TargetType.GetMethod("Parse", new[] { typeof(string) });
            MethodCallExpression CallExpression = Expression.Call(ParseMetod, new[] { SourceExpression });
            return CallExpression;
        }

        /// <summary>
        /// Creates an Expression that parses a string to a number
        /// </summary>
        /// <param name="SourceExpression"></param>
        /// <param name="TargetType"></param>
        /// <param name="Provider"></param>
        /// <returns></returns>
        private static MethodCallExpression GetNumberParseExpression(Expression SourceExpression, Type TargetType, CultureInfo Culture)
        {
            MethodInfo ParseMetod = TargetType.GetMethod("Parse", new[] { typeof(string), typeof(NumberFormatInfo) });
            ConstantExpression ProviderExpression = Expression.Constant(Culture.NumberFormat, typeof(NumberFormatInfo));
            MethodCallExpression CallExpression = Expression.Call(ParseMetod, new[] { SourceExpression, ProviderExpression });
            return CallExpression;
        }

        /// <summary>
        /// Creates an Expression that parses a string to a DateTime
        /// </summary>
        /// <param name="SourceExpression"></param>
        /// <param name="TargetType"></param>
        /// <param name="Culture"></param>
        /// <returns></returns>
        private static MethodCallExpression GetDateTimeParseExpression(Expression SourceExpression, Type TargetType, CultureInfo Culture)
        {
            MethodInfo ParseMetod = TargetType.GetMethod("Parse", new[] { typeof(string), typeof(DateTimeFormatInfo) });
            ConstantExpression ProviderExpression = Expression.Constant(Culture.DateTimeFormat, typeof(DateTimeFormatInfo));
            MethodCallExpression CallExpression = Expression.Call(ParseMetod, new[] { SourceExpression, ProviderExpression });
            return CallExpression;
        }

        /// <summary>
        /// Creates an Expression that parses a string
        /// </summary>
        /// <param name="SourceExpression"></param>
        /// <param name="TargetType"></param>
        /// <param name="Provider"></param>
        /// <returns></returns>
        private static Expression GetParseExpression(Expression SourceExpression, Type TargetType, CultureInfo Culture)
        {
            Type UnderlyingType = GetUnderlyingType(TargetType);
            if (UnderlyingType.IsEnum)
            {
                MethodCallExpression ParsedEnumExpression = GetEnumParseExpression(SourceExpression, UnderlyingType);
                //Enum.Parse returns an object that needs to be unboxed
                return Expression.Unbox(ParsedEnumExpression, TargetType);
            }
            else
            {
                Expression ParseExpression = default(Expression);
                switch (UnderlyingType.FullName)
                {
                    case "System.Byte":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                    case "System.SByte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        ParseExpression = GetNumberParseExpression(SourceExpression, UnderlyingType, Culture);
                        break;
                    case "System.DateTime":
                        ParseExpression = GetDateTimeParseExpression(SourceExpression, UnderlyingType, Culture);
                        break;
                    case "System.Boolean":
                    case "System.Char":
                        ParseExpression = GetGenericParseExpression(SourceExpression, UnderlyingType);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Conversion from {0} to {1} is not supported", "String", TargetType));
                }
                if (Nullable.GetUnderlyingType(TargetType) == null)
                {
                    return ParseExpression;
                }
                else
                {
                    //Convert to nullable if necessary
                    return Expression.Convert(ParseExpression, TargetType);
                }
            }
        }

        /// <summary>
        /// Returns Underlying Type if it's a nullable, otherwise the type itself
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        private static Type GetUnderlyingType(Type Type)
        {
            if (Nullable.GetUnderlyingType(Type) == null)
            {
                return Type;
            }
            else
            {
                return Nullable.GetUnderlyingType(Type);
            }
        }
        #endregion

        #region"Mapper"

        private static bool IsElementaryType(this Type t)
        {
            return ElementaryTypes.Contains(t);
        }
        readonly static HashSet<Type> ElementaryTypes = LoadElementaryTypes();
        private static HashSet<Type> LoadElementaryTypes()
        {
            HashSet<Type> TypeSet = new HashSet<Type> {
      typeof(String),
      typeof(Byte),
      typeof(SByte),
      typeof(Int16),
      typeof(Int32),
      typeof(Int64),
      typeof(UInt16),
      typeof(UInt32),
      typeof(UInt64),
      typeof(Single),
      typeof(Double),
      typeof(Decimal),
      typeof(DateTime),
      typeof(Guid),
      typeof(Boolean),
      typeof(TimeSpan),
      typeof(Nullable<Byte>),
      typeof(Nullable<SByte>),
      typeof(Nullable<Int16>),
      typeof(Nullable<Int32>),
      typeof(Nullable<Int64>),
      typeof(Nullable<UInt16>),
      typeof(Nullable<UInt32>),
      typeof(Nullable<UInt64>),
      typeof(Nullable<Single>),
      typeof(Nullable<Double>),
      typeof(Nullable<Decimal>),
      typeof(Nullable<DateTime>),
      typeof(Nullable<Guid>),
      typeof(Nullable<Boolean>),
      typeof(Nullable<TimeSpan>)
   };
            return TypeSet;
        }

        /// <summary>
        /// Gets an Expression that checks if the current RecordField is null
        /// </summary>
        /// <param name="RecordType">The Type of the Record</param>
        /// <param name="RecordInstance">The Record instance</param>
        /// <param name="Ordinal">The index of the parameter</param>
        /// <returns>MethodCallExpression</returns>
        private static MethodCallExpression GetNullCheckExpression(Type RecordType, ParameterExpression RecordInstance, int Ordinal)
        {
            MethodInfo GetNullValueMethod = RecordType.GetMethod("IsDBNull", new Type[] { typeof(int) });
            MethodCallExpression NullCheckExpression = Expression.Call(RecordInstance, GetNullValueMethod, Expression.Constant(Ordinal, typeof(int)));
            return NullCheckExpression;
        }

        /// <summary>
        /// Gets an Expression that represents the getter method for the RecordField
        /// </summary>
        /// <param name="RecordType">The Type of the Record</param>
        /// <param name="RecordInstanceExpression">The Record instance</param>
        /// <param name="Ordinal">The index of the parameter</param>
        /// <param name="RecordFieldType">The Type of the RecordField</param>
        /// <returns></returns>
        private static Expression GetRecordFieldExpression(Type RecordType, ParameterExpression RecordInstanceExpression, int Ordinal, Type RecordFieldType)
        {
            MethodInfo GetValueMethod = default(MethodInfo);

            switch (RecordFieldType.FullName)
            {
                case "System.Boolean":
                    GetValueMethod = RecordType.GetMethod("GetBoolean", new Type[] { typeof(int) });
                    break;
                case "System.Byte":
                    GetValueMethod = RecordType.GetMethod("GetByte", new Type[] { typeof(int) });
                    break;
                case "System.Byte[]":
                    GetValueMethod = typeof(HelperFunctions).GetMethod("RecordFieldToBytes", new Type[] { typeof(IDataRecord), typeof(int) });
                    break;
                case "System.Char":
                    GetValueMethod = RecordType.GetMethod("GetChar", new Type[] { typeof(int) });
                    break;
                case "System.DateTime":
                    GetValueMethod = RecordType.GetMethod("GetDateTime", new Type[] { typeof(int) });
                    break;
                case "System.Decimal":
                    GetValueMethod = RecordType.GetMethod("GetDecimal", new Type[] { typeof(int) });
                    break;
                case "System.Double":
                    GetValueMethod = RecordType.GetMethod("GetDouble", new Type[] { typeof(int) });
                    break;
                case "System.Single":
                    GetValueMethod = RecordType.GetMethod("GetFloat", new Type[] { typeof(int) });
                    break;
                case "System.Guid":
                    GetValueMethod = RecordType.GetMethod("GetGuid", new Type[] { typeof(int) });
                    break;
                case "System.Int16":
                    GetValueMethod = RecordType.GetMethod("GetInt16", new Type[] { typeof(int) });
                    break;
                case "System.Int32":
                    GetValueMethod = RecordType.GetMethod("GetInt32", new Type[] { typeof(int) });
                    break;
                case "System.Int64":
                    GetValueMethod = RecordType.GetMethod("GetInt64", new Type[] { typeof(int) });
                    break;
                case "System.String":
                    GetValueMethod = RecordType.GetMethod("GetString", new Type[] { typeof(int) });
                    break;
                default:
                    GetValueMethod = RecordType.GetMethod("GetValue", new Type[] { typeof(int) });
                    break;
            }

            Expression RecordFieldExpression;
            if (object.ReferenceEquals(RecordFieldType, typeof(byte[])))
            {
                RecordFieldExpression = Expression.Call(GetValueMethod, new Expression[] { RecordInstanceExpression, Expression.Constant(Ordinal, typeof(int)) });
            }
            else
            {
                RecordFieldExpression = Expression.Call(RecordInstanceExpression, GetValueMethod, Expression.Constant(Ordinal, typeof(int)));
            }
            return RecordFieldExpression;
        }

        /// <summary>
        /// Returns The FieldNameAttribute if existing
        /// </summary>
        /// <param name="Member">MemberInfo</param>
        /// <returns>String</returns>
        private static string GetFieldNameAttribute(MemberInfo Member)
        {
            if (Member.GetCustomAttributes(typeof(FieldNameAttribute), true).Count() > 0)
            {
                return ((FieldNameAttribute)Member.GetCustomAttributes(typeof(FieldNameAttribute), true)[0]).FieldName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks if the Field name matches the Member name or Members FieldNameAttribute
        /// </summary>
        /// <param name="Member">The Member of the Instance to check</param>
        /// <param name="Name">The Name to compare with</param>
        /// <returns>True if Fields match</returns>
        /// <remarks>FieldNameAttribute takes precedence over TargetMembers name.</remarks>
        private static bool MemberMatchesName(MemberInfo Member, string Name)
        {
            string FieldnameAttribute = GetFieldNameAttribute(Member);
            return FieldnameAttribute.ToLower() == Name.ToLower() || Member.Name.ToLower() == Name.ToLower();
        }

        /// <summary>
        /// Returns an Expression representing the value to set the TargetProperty to
        /// </summary>
        /// <remarks>Prepares the parameters to call the other overload</remarks>
        private static Expression GetTargetValueExpression(
            IDataRecord RecordInstance,
            CultureInfo Culture,
            Type RecordType,
            ParameterExpression RecordInstanceExpression,
            DataTable SchemaTable,
            int Ordinal,
            Type TargetMemberType)
        {
            Type RecordFieldType = RecordInstance.GetFieldType(Ordinal);
            bool AllowDBNull = Convert.ToBoolean(SchemaTable.Rows[Ordinal]["AllowDBNull"]);
            Expression RecordFieldExpression = GetRecordFieldExpression(RecordType, RecordInstanceExpression, Ordinal, RecordFieldType);
            Expression ConvertedRecordFieldExpression = GetConversionExpression(RecordFieldType, RecordFieldExpression, TargetMemberType, Culture);
            MethodCallExpression NullCheckExpression = GetNullCheckExpression(RecordType, RecordInstanceExpression, Ordinal);

            //Create an expression that assigns the converted value to the target
            Expression TargetValueExpression = default(Expression);
            if (AllowDBNull)
            {
                TargetValueExpression = Expression.Condition(
                    NullCheckExpression,
                    Expression.Default(TargetMemberType),
                    ConvertedRecordFieldExpression,
                    TargetMemberType
                    );
            }
            else
            {
                TargetValueExpression = ConvertedRecordFieldExpression;
            }
            return TargetValueExpression;
        }

        /// <summary>
        /// Creates a delegate that creates an instance of Target from the supplied DataRecord
        /// </summary>
        /// <param name="RecordInstance">An instance of a DataRecord</param>
        /// <returns>A Delegate that creates a new instance of Target with the values set from the supplied DataRecord</returns>
        /// <remarks></remarks>
        private static Func<IDataRecord, Target> GetInstanceCreator<Target>(IDataRecord RecordInstance, CultureInfo Culture, Boolean MustMapAllProperties)
        {
            Type RecordType = typeof(IDataRecord);
            ParameterExpression RecordInstanceExpression = Expression.Parameter(RecordType, "SourceInstance");
            Type TargetType = typeof(Target);
            DataTable SchemaTable = ((IDataReader)RecordInstance).GetSchemaTable();
            Expression Body = default(Expression);

            //The actual names for Tuples are System.Tuple`1,System.Tuple`2 etc where the number stands for the number of Parameters 
            //This crashes whenever Microsoft creates a class in the System Namespace called Tuple`duple 
            if (TargetType.FullName.StartsWith("System.Tuple`"))
            {
                ConstructorInfo[] Constructors = TargetType.GetConstructors();
                if (Constructors.Count() != 1)
                    throw new ArgumentException("Tuple must have one Constructor");
                var Constructor = Constructors[0];

                var Parameters = Constructor.GetParameters();
                if (Parameters.Length > 7)
                    throw new NotSupportedException("Nested Tuples are not supported");

                Expression[] TargetValueExpressions = new Expression[Parameters.Length];
                for (int Ordinal = 0; Ordinal < Parameters.Length; Ordinal++)
                {
                    var ParameterType = Parameters[Ordinal].ParameterType;
                    if (Ordinal >= RecordInstance.FieldCount)
                    {
                        if (MustMapAllProperties) { throw new ArgumentException("Tuple has more fields than the DataReader"); }
                        TargetValueExpressions[Ordinal] = Expression.Default(ParameterType);
                    }
                    else
                    {
                        TargetValueExpressions[Ordinal] = GetTargetValueExpression(
                                                        RecordInstance,
                                                        Culture,
                                                        RecordType,
                                                        RecordInstanceExpression,
                                                        SchemaTable,
                                                        Ordinal,
                                                        ParameterType);
                    }
                }
                Body = Expression.New(Constructor, TargetValueExpressions);
            }
            //Find out if SourceType is an elementary Type.
            else if (TargetType.IsElementaryType())
            {
                //If you try to map an elementary type, e.g. ToList<Int32>, there is no name to map on. So to avoid error we map to the first field in the datareader
                //If this is wrong, it is the query that's wrong.
                const int Ordinal = 0;
                Expression TargetValueExpression = GetTargetValueExpression(
                                                       RecordInstance,
                                                       Culture,
                                                       RecordType,
                                                       RecordInstanceExpression,
                                                       SchemaTable,
                                                       Ordinal,
                                                       TargetType);

                ParameterExpression TargetExpression = Expression.Variable(TargetType, "Target");
                Expression AssignExpression = Expression.Assign(TargetExpression, TargetValueExpression);
                Body = Expression.Block(new ParameterExpression[] { TargetExpression }, AssignExpression);
            }
            else
            {
                //Loop through the Properties in the Target and the Fields in the Record to check which ones are matching
                SortedDictionary<int, MemberBinding> Bindings = new SortedDictionary<int, MemberBinding>();
                foreach (FieldInfo TargetMember in TargetType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    Action work = delegate
                    {
                        for (int Ordinal = 0; Ordinal < RecordInstance.FieldCount; Ordinal++)
                        {
                            //Check if the RecordFieldName matches the TargetMember
                            if (MemberMatchesName(TargetMember, RecordInstance.GetName(Ordinal)))
                            {
                                Expression TargetValueExpression = GetTargetValueExpression(
                                                                       RecordInstance,
                                                                       Culture,
                                                                       RecordType,
                                                                       RecordInstanceExpression,
                                                                       SchemaTable,
                                                                       Ordinal,
                                                                       TargetMember.FieldType);

                                //Create a binding to the target member
                                MemberAssignment BindExpression = Expression.Bind(TargetMember, TargetValueExpression);
                                Bindings.Add(Ordinal, BindExpression);
                                return;
                            }
                        }
                        //If we reach this code the targetmember did not get mapped
                        if (MustMapAllProperties)
                        {
                            throw new ArgumentException(String.Format("TargetField {0} is not matched by any field in the DataReader", TargetMember.Name));
                        }
                    };
                    work();
                }

                foreach (PropertyInfo TargetMember in TargetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (TargetMember.CanWrite)
                    {
                        Action work = delegate
                        {
                            for (int Ordinal = 0; Ordinal < RecordInstance.FieldCount; Ordinal++)
                            {
                                //Check if the RecordFieldName matches the TargetMember
                                if (MemberMatchesName(TargetMember, RecordInstance.GetName(Ordinal)))
                                {
                                    Expression TargetValueExpression = GetTargetValueExpression(
                                                                           RecordInstance,
                                                                           Culture,
                                                                           RecordType,
                                                                           RecordInstanceExpression,
                                                                           SchemaTable,
                                                                           Ordinal,
                                                                           TargetMember.PropertyType);

                                    //Create a binding to the target member
                                    MemberAssignment BindExpression = Expression.Bind(TargetMember, TargetValueExpression);
                                    Bindings.Add(Ordinal, BindExpression);
                                    return;
                                }
                            }
                            //If we reach this code the targetmember did not get mapped
                            if (MustMapAllProperties)
                            {
                                throw new ArgumentException(String.Format("TargetProperty {0} is not matched by any Field in the DataReader", TargetMember.Name));
                            }
                        };
                        work();
                    }
                }

                //Create a memberInitExpression that Creates a new instance of Target using bindings to the DataRecord
                Body = Expression.MemberInit(Expression.New(TargetType), Bindings.Values);
            }
            //Compile the Expression to a Delegate
            return Expression.Lambda<Func<IDataRecord, Target>>(Body, RecordInstanceExpression).Compile();
        }

        #endregion
        #endregion

        #region "Nested Classes"

        private class DataField
        {
            private readonly Type _fieldType;

            private readonly string _fieldName;
            public DataField(string FieldName, Type FieldType)
            {
                _fieldName = FieldName;
                _fieldType = FieldType;
            }
        }

        private class DataFieldList : List<DataField>
        {

            public DataFieldList(CultureInfo Culture, Boolean MustMapAllProperties)
            {
                this.Culture = Culture;
                this.MustMapAllProperties = MustMapAllProperties;
            }

            public CultureInfo Culture { get; set; }
            public Boolean MustMapAllProperties { get; set; }

            public override bool Equals(object obj)
            {
                DataFieldList list = obj as DataFieldList;
                if (list == null)
                {
                    return false;
                }
                else
                {
                    return this.MustMapAllProperties.Equals(list.MustMapAllProperties) && this.Culture.Equals(list.Culture) && Enumerable.SequenceEqual<DataField>(this, list);
                }
            }

            public override int GetHashCode()
            {
                Int64 hash = Culture.GetHashCode() ^ Convert.ToInt32(MustMapAllProperties);
                int i = 0;
                foreach (DataField Item in this)
                {
                    hash = hash * 31 + Item.GetHashCode();
                    i += 1;
                    if (i < 7)
                    {
                        hash = unchecked((Int32)(hash ^ hash >> 32));
                        i = 0;
                    }
                }
                return unchecked((Int32)(hash ^ hash >> 32));
            }
        }

        private sealed class ReaderMapperCache<Target> : Dictionary<Int32, Func<IDataRecord, Target>>
        {
            private ReaderMapperCache()
            {
            }
            private static readonly object SyncRoot = new object();

            private static readonly Dictionary<DataFieldList, Func<IDataRecord, Target>> _Creators = new Dictionary<DataFieldList, Func<IDataRecord, Target>>();
            private static DataFieldList GetDataFieldList(IDataRecord RecordInstance, CultureInfo Culture, Boolean MustMapAllProperties)
            {
                DataFieldList List = new DataFieldList(Culture, MustMapAllProperties);
                DataField Field = null;
                for (int Ordinal = 0; Ordinal < RecordInstance.FieldCount; Ordinal++)
                {
                    Field = new DataField(RecordInstance.GetName(Ordinal), RecordInstance.GetFieldType(Ordinal));
                    List.Add(Field);
                }
                return List;
            }

            static internal Func<IDataRecord, Target> GetCreator(IDataRecord RecordInstance, CultureInfo Culture, Boolean MustMapAllProperties)
            {
                DataFieldList List = GetDataFieldList(RecordInstance, Culture, MustMapAllProperties);
                if (!_Creators.ContainsKey(List))
                {
                    lock (SyncRoot)
                    {
                        if (!_Creators.ContainsKey(List))
                        {
                            _Creators.Add(List, GetInstanceCreator<Target>(RecordInstance, Culture, MustMapAllProperties));
                        }
                    }
                }
                return _Creators[List];
            }
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    class FieldNameAttribute : Attribute
    {
        private readonly string _FieldName;
        public string FieldName
        {
            get { return _FieldName; }
        }

        public FieldNameAttribute(string FieldName)
        {
            _FieldName = FieldName;
        }
    }

    public static class HelperFunctions
    {
        public static byte[] RecordFieldToBytes(IDataRecord Reader, int Column)
        {
            long BlobSize = Reader.GetBytes(Column, 0, null, 0, 0);
            if (BlobSize > int.MaxValue)
                throw new ArgumentOutOfRangeException("MemoryStream cannot be larger than " + int.MaxValue);

            byte[] Buffer = new byte[Convert.ToInt32(BlobSize - 1) + 1];
            Reader.GetBytes(Column, 0, Buffer, 0, Buffer.Length);
            return Buffer;
        }
    }

    public static class Extensions
    {
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
    }
}
