using System;
using System.IO;

namespace FlightChecker.Repository
{
    public abstract class CsvRepository<T>
    {
        internal string _source;
        internal const string _delimiter = ";";
        internal readonly Type[] _supportedTypes = { typeof(String), typeof(Decimal), typeof(DateTime), typeof(DateTime?) };


        public CsvRepository(string source)
        {
            _source = String.Format(@"Repository/csv/{0}.csv", source);
            var _sourcePart = String.Format("~\\Repository\\csv\\{0}.csv", source);

            //_source = System.Web.HttpContext.Current.Request.MapPath(_sourcePart);

            if (!File.Exists(_source))
            {
                throw new ArgumentException(String.Format("The requested repository {0} doesn't exist", source));
            }
        }     
                
        public bool IsTypeSupportedByRepository(Type type)
        {
            return Array.IndexOf(_supportedTypes, type) != -1;
        }   

        public T GiveMeAnObject(string[] properties, string[] values)
        {
            try
            {
                T instance = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < properties.Length; i++)
                {
                    if (typeof(T).GetProperty(properties[i]) == null)
                    {
                        throw new NotImplementedException("Type not supported by Repository");
                    }

                    if (!IsTypeSupportedByRepository(typeof(T).GetProperty(properties[i]).PropertyType))
                    {
                        throw new NotImplementedException("Type not supported by Repository");
                    }

                    var property = typeof(T).GetProperty(properties[i]);

                    if (property.PropertyType == typeof(Decimal))
                    {
                        var value = Decimal.Parse(values[i]);
                        property.SetValue(instance, value);
                    }

                    if (property.PropertyType == typeof(DateTime))
                    {
                        var value = DateTime.Parse(values[i]);
                        property.SetValue(instance, value);
                    }

                    if (property.PropertyType == typeof(DateTime?))
                    {
                        DateTime parsedDate;
                        var value = DateTime.TryParse(values[i], out parsedDate) ? parsedDate : (DateTime?)null;
                        property.SetValue(instance, value);
                    }

                    if (property.PropertyType == typeof(String))
                    {
                        var value = values[i];
                        property.SetValue(instance, value);
                    }
                }
                return instance;
            }

            catch (NullReferenceException)
            {
                throw new NotImplementedException("Mismatch between Type and Type");
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        
    }
}