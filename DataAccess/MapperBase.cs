using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
    public abstract class MapperBase<T>
    {
        protected abstract T Map(IDataRecord record);

        public Collection<T> MapAll(IDataReader reader)
        {
            Collection<T> collection = new Collection<T>();

            while (reader.Read())
            {
                try
                {
                    collection.Add(Map(reader));
                }
                catch (Exception e)
                {
                    throw e;

                    // NOTE:
                    // consider handling exeption here instead of re-throwing
                    // if graceful recovery can be accomplished
                }
            }
            return collection;
        }
    }
}