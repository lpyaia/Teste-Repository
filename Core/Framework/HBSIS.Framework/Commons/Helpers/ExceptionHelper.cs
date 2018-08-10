using System;
using System.Data.SqlClient;

namespace HBSIS.Framework.Commons.Helper
{
    public static class ExceptionHelper
    {
        public static SqlException GetSqlException(this Exception ex)
        {
            if (ex.GetType() == typeof(SqlException))
                return ex as SqlException;

            if (ex.InnerException != null)
                return GetSqlException(ex.InnerException);

            return null;
        }
    }
}