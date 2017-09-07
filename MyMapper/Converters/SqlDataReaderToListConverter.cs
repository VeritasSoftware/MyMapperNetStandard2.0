using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMapper.Converters
{
    /// <summary>
    /// SqlDataReader to List converter
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public class SqlDataReaderToListConverter<TEntity> : ITypeConverter<SqlDataReader, IList<TEntity>>, ITypeConverterAsync<SqlDataReader, IList<TEntity>>
        where TEntity : class, new()
    {
        static ConcurrentDictionary<Type, List<PropertyInfo>> dictionaryEntityPropertyInfos;

        public IList<TEntity> Convert(SqlDataReader source)
        {
            List<PropertyInfo> entityPropertyInfos;

            List<TEntity> list = new List<TEntity>();

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

        public async Task<IList<TEntity>> ConvertAsync(SqlDataReader source)
        {
            return await Task.Run(() =>
            {
                List<PropertyInfo> entityPropertyInfos;

                List<TEntity> list = new List<TEntity>();

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
            });
        }
    }
}
