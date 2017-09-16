using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyMapper.Converters
{
    /// <summary>
    /// DataRowToEntityConverter : Converts a datarow to an entity
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public class DataRowToEntityConverter<TEntity> : ITypeConverter<DataRow, TEntity>
#if !NET4
                                                     ,ITypeConverterAsync<DataRow, TEntity>
#endif
        where TEntity : class, new()
    {
        static ConcurrentDictionary<Type, List<PropertyInfo>> dictionaryEntityPropertyInfos;

        public TEntity Convert(DataRow source)
        {
            List<PropertyInfo> entityPropertyInfos;

            TEntity obj = new TEntity();

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

            foreach (PropertyInfo propertyInfo in entityPropertyInfos)
            {
                object rowVal = null;

                try
                {
                    rowVal = source[propertyInfo.Name];
                }
                catch (Exception)
                {
                    continue;
                }

                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    try
                    {
                        object value = System.Convert.ChangeType(rowVal, propertyInfo.PropertyType);
                        propertyInfo.SetValue(obj, value, null);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return obj;
        }

#if !NET4
        public async Task<TEntity> ConvertAsync(DataRow source)
        {
            return await Task.Run(() =>
            {
                List<PropertyInfo> entityPropertyInfos;

                TEntity obj = new TEntity();

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

                foreach (PropertyInfo propertyInfo in entityPropertyInfos)
                {
                    object rowVal = null;

                    try
                    {
                        rowVal = source[propertyInfo.Name];
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        try
                        {
                            object value = System.Convert.ChangeType(rowVal, propertyInfo.PropertyType);
                            propertyInfo.SetValue(obj, value, null);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                return obj;
            });
        }
#endif
    }
}
