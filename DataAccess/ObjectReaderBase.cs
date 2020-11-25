using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
   public abstract class ObjectReaderBase<T>
    {
        protected abstract IDbConnection GetConnection();
        protected abstract CommandType CommandType { get; }
        protected abstract Collection<IDataParameter> GetParameters(IDbCommand command);
        protected abstract MapperBase<T> GetMapper();

        protected string CommandText { get; set; }
        public string String { get; private set; }

        public Collection<T> Execute()
        {
            Collection<T> collection = new Collection<T>();

            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = this.CommandText;
                command.CommandType = this.CommandType;
                
                foreach (IDataParameter param in this.GetParameters(command))
                    command.Parameters.Add(param);

                try
                {
                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            MapperBase<T> mapper = GetMapper();
                            collection = mapper.MapAll(reader);
                            return collection;
                        }
                        catch (Exception e)
                        {
                            Trace.TraceError("{0}: Error executing SQL query. Error = {1}", DateTime.Now.ToString(), e.Message);
                            Trace.TraceError("{0}: Error executing SQL query: Query = {1}", DateTime.Now.ToString(), this.CommandText);
                            throw e;


                            // NOTE: 
                            // consider handling exeption here instead of re-throwing
                            // if graceful recovery can be accomplished
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("{0}: Error executing SQL query. Error = {1}", DateTime.Now.ToString(), e.Message);
                    Trace.TraceError("{0}: Error executing SQL query: Query = {1}", DateTime.Now.ToString(), this.CommandText);

                    throw e;

                    // NOTE: 
                    // consider handling exeption here instead of re-throwing
                    // if graceful recovery can be accomplished
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        


        public int ExecuteNonQuery()
        {
            decimal retId = 0;
            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = this.CommandText;
                command.CommandType = this.CommandType;

                foreach (IDataParameter param in this.GetParameters(command))
                    command.Parameters.Add(param);

                try
                {
                    connection.Open();

                    try
                    {
                        int rows_affected = command.ExecuteNonQuery();

                        // get id inserted
                        command.CommandText = "select SCOPE_IDENTITY() AS Id;";
                        IDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            retId = (DBNull.Value == reader["Id"]) ? 0 : (decimal)reader["Id"];
                        }
                        else
                            retId = 0;

                        return (int)retId;
                    }
                    catch (SqlException sqle)
                    {
                        Trace.TraceError("{0}: Error executing SQL query. Error = {1}", DateTime.Now.ToString(), sqle.Message);
                        Trace.TraceError("{0}: Error executing SQL query: Query = {1}", DateTime.Now.ToString(), this.CommandText);
                        throw sqle;

                        // NOTE: 
                        // consider handling exeption here instead of re-throwing
                        // if graceful recovery can be accomplished
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e.Message);
                        Trace.TraceError("{0}: Error executing SQL query. Error = {1}", DateTime.Now.ToString(), e.Message);
                        Trace.TraceError("{0}: Error executing SQL query: Query = {1}", DateTime.Now.ToString(), this.CommandText);
                        throw;
                    }
                    finally
                    {

                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("{0}: Error executing SQL query. Error = {1}", DateTime.Now.ToString(), e.Message);
                    Trace.TraceError("{0}: Error executing SQL query: Query = {1}", DateTime.Now.ToString(), this.CommandText);

                    throw e;

                    // NOTE: 
                    // consider handling exeption here instead of re-throwing
                    // if graceful recovery can be accomplished
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}