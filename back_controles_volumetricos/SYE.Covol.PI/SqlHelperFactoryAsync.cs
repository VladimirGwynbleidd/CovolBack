using Microsoft.Extensions.Logging;
using SYE.Covol.PI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace SYE.Covol
{
    public class SqlHelperFactoryAsync
    {
        public ILogger Logger { get; }

        public SqlHelperFactoryAsync(ILogger logger = null)
        {
            Logger = logger;
        }

        public async Task<List<T>> ExecuteList<T>(string procedure, Dictionary<string, object> parameters) where T : class, new()
        {
            List<T> lstEntity = null;
            try
            {

                using (SqlConnection _conn = await new SqlHelperConnection(Logger).connectionAsync())
                {
                    if (_conn.State == ConnectionState.Closed)
                    {
                        await _conn.OpenAsync();
                    }
                    using (SqlCommand Command = new SqlCommand(procedure, _conn))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            Command.Parameters.Clear();
                            foreach (KeyValuePair<string, object> kvp in parameters)
                            {
                                Command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                            }
                        }
                        using (SqlDataReader reader = await Command.ExecuteReaderAsync())
                        {
                            return reader.Select(r => DelegadoAccion<T>(r)).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger?.LogError($"Error en sp {procedure} se produjo el siguiente error: {ex.Message}");

            }
            return lstEntity;
        }


        public async Task<int> ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters)
        {
            int returnObject = 0;
            using (SqlConnection _conn = await new SqlHelperConnection(Logger).connectionAsync())
            {
               
                using (SqlCommand Command = new SqlCommand(procedureName, _conn))
                {
                    try
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            Command.Parameters.Clear();
                            foreach (KeyValuePair<string, object> kvp in parameters)
                            {
                                Command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                            }
                        }
                        returnObject = await Command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ext)
                    {
                        Logger?.LogError($"Error en sp {procedureName} se produjo el siguiente error: {ext.Message}");
                        throw new ArgumentException(ext.Message, ext);
                    }
                    finally
                    {
                        if (_conn.State == ConnectionState.Open)
                        {
                            Logger?.LogWarning("Cerrando conexión a la base de datos en modo asincrono ExecuteNonQuery");
                            _conn.Close();
                        }
                    }
                }
                return returnObject;
            }
        }


        public async Task<object> ExecuteScalar(string procedureName, Dictionary<string, object> parameters)
        {
            object returnObject = null;
            using (SqlConnection _conn = await new SqlHelperConnection(Logger).connectionAsync())
            {
                
                using (SqlCommand Command = new SqlCommand(procedureName, _conn))
                {
                    try
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                     
                        if (parameters != null)
                        {
                            Command.Parameters.Clear();
                            foreach (KeyValuePair<string, object> kvp in parameters)
                            {
                                Command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                            }
                        }
                        returnObject = await Command.ExecuteScalarAsync();
                     
                    }
                    catch (Exception ext)
                    {
                        Logger?.LogError("Error en sp " + procedureName + " se produjo el siguiente error: " + ext.Message);
                        throw new ArgumentException(ext.Message, ext);
                    }
                    finally
                    {
                        if (_conn.State == ConnectionState.Open)
                        {
                            Logger?.LogWarning("Cerrando conexión a la base de datos");
                            _conn.Close();
                        }
                    }
                }
                return returnObject;
            }
        }



        //public static async Task<int> ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters)
        //{
        //    int returnObject = 0;
        //    using (SqlConnection _conn = await SqlHelperConnection.connectionAsync())
        //    {
        //        if (_conn.State == ConnectionState.Closed)
        //        {
        //            await _conn.OpenAsync();
        //        }

        //        using (SqlCommand Command = new SqlCommand(procedureName, _conn))
        //        {
        //            SqlTransaction transaction = null;
        //            try
        //            {
        //                transaction = _conn.BeginTransaction();
        //                Command.CommandType = CommandType.StoredProcedure;
        //                Command.Transaction = transaction;
        //                if (parameters != null)
        //                {
        //                    Command.Parameters.Clear();
        //                    foreach (KeyValuePair<string, object> kvp in parameters)
        //                    {
        //                        Command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
        //                    }
        //                }
        //                returnObject = await Command.ExecuteNonQueryAsync();
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (transaction != null && transaction.Connection.State == ConnectionState.Open)
        //                {
        //                    try
        //                    {
        //                        transaction.Rollback();
        //                    }
        //                    catch (Exception ext)
        //                    {
        //                        Log.Error(ext.Message, ext);
        //                    }
        //                }
        //                Log.Error("Error en sp " + procedureName + " se produjo el siguiente error: " + ex.Message);
        //            }
        //            finally
        //            {
        //                if (transaction != null)
        //                {
        //                    transaction.Dispose();
        //                }
        //            }
        //        }
        //        return returnObject;
        //    }
        //}

        //public static async Task<object> ExecuteScalar(string procedureName, Dictionary<string, object> parameters)
        //{
        //    object returnObject = null;
        //    using (SqlConnection _conn = await SqlHelperConnection.connectionAsync())
        //    {
        //        if (_conn.State == ConnectionState.Closed)
        //        {
        //            await _conn.OpenAsync();
        //        }
        //        using (SqlCommand Command = new SqlCommand(procedureName, _conn))
        //        {
        //            SqlTransaction transaction = null;
        //            try
        //            {
        //                transaction = _conn.BeginTransaction();
        //                Command.CommandType = CommandType.StoredProcedure;
        //                Command.Transaction = transaction;
        //                if (parameters != null)
        //                {
        //                    Command.Parameters.Clear();
        //                    foreach (KeyValuePair<string, object> kvp in parameters)
        //                    {
        //                        Command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
        //                    }
        //                }
        //                returnObject = await Command.ExecuteScalarAsync();
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (transaction != null && transaction.Connection.State == ConnectionState.Open)
        //                {
        //                    try
        //                    {
        //                        transaction.Rollback();
        //                    }
        //                    catch (Exception ext)
        //                    {
        //                        Log.Error(ext.Message, ext);
        //                    }
        //                }
        //                Log.Error("Error en sp " + procedureName + " se produjo el siguiente error: " + ex.Message);
        //            }
        //            finally
        //            {
        //                if (transaction != null)
        //                {
        //                    transaction.Dispose();
        //                }
        //            }
        //        }
        //        return returnObject;
        //    }
        //}

        public static T DelegadoAccion<T>(SqlDataReader dr) where T : class, new()
        {
            T obj = new T();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                PropertyInfo property = obj.GetType().GetProperty(dr.GetName(i), BindingFlags.SetProperty | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (!dr.IsDBNull(i) && property != null)
                {
                    switch (Type.GetTypeCode(property.PropertyType))
                    {
                        case TypeCode.String:
                            property.SetValue(obj, dr.GetString(i), null);
                            break;
                        case TypeCode.Int32:
                            property.SetValue(obj, (object)dr.GetFieldType(i) == typeof(int) ? dr.GetInt32(i) : (int)dr.GetInt64(i), null);
                            break;
                        case TypeCode.UInt64:
                            property.SetValue(obj, dr.GetInt64(i), null);
                            break;
                        case TypeCode.Boolean:
                            property.SetValue(obj, dr.GetBoolean(i), null);
                            break;
                        case TypeCode.Double:
                            property.SetValue(obj, dr.GetDouble(i), null);
                            break;
                        case TypeCode.Decimal:
                            property.SetValue(obj, dr.GetDecimal(i), null);
                            break;
                        case TypeCode.DateTime:
                            property.SetValue(obj, dr.GetDateTime(i).ToString("yyyy-MM-ddTHH:mm:ss").ToDateTime(), null);
                            break;
                        default:
                            property.SetValue(obj, dr.GetFloat(i), null);
                            break;
                    }
                }
            }
            return obj;
        }
    }
}