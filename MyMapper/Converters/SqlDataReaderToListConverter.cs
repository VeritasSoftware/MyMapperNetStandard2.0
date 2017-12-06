using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyMapper.Converters
{
    /// <summary>
    /// SqlDataReader to List converter
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public class SqlDataReaderToListConverter<TEntity> : ITypeConverter<SqlDataReader, List<TEntity>>
#if !NET4
                                                            , ITypeConverterAsync<SqlDataReader, List<TEntity>>
#endif
        where TEntity : class, new()
    {
        static ConcurrentDictionary<Type, List<PropertyInfo>> dictionaryEntityPropertyInfos;

        public List<TEntity> Convert(SqlDataReader source, List<TEntity> destination = null)
        {
            List<PropertyInfo> entityPropertyInfos;

            List<TEntity> list;

            if (destination == null)
            {
                list = new List<TEntity>();
            }
            else
                list = destination.ToList();
            

            if (dictionaryEntityPropertyInfos == null)
                dictionaryEntityPropertyInfos = new ConcurrentDictionary<Type, List<PropertyInfo>>();

            if (!dictionaryEntityPropertyInfos.ContainsKey(typeof(TEntity)))
            {
                entityPropertyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

                dictionaryEntityPropertyInfos.GetOrAdd(typeof(TEntity), entityPropertyInfos);
            }
            else
            {
                dictionaryEntityPropertyInfos.TryGetValue(typeof(TEntity), out entityPropertyInfos);
            }

            if (source != null)
            {
                while (source.Read())
                {
                    TEntity entity = new TEntity();

                    for (int i = 0; i < source.FieldCount; i++)
                    {
                        var columnName = source.GetName(i);
                        PropertyInfo propertyInfo = null;

                        try
                        {
                            propertyInfo = entityPropertyInfos.SingleOrDefault(pi => pi.Name == columnName);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            try
                            {
                                object value = System.Convert.ChangeType(source[columnName], Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);

                                propertyInfo.SetValue(entity, value, null);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    list.Add(entity);
                }
            }

            return list;
        }
#if !NET4
        public async Task<List<TEntity>> ConvertAsync(SqlDataReader source, List<TEntity> destination = null)
        {
            return await Task.Run(() => Convert(source, destination));
        }
#endif
    }
}
