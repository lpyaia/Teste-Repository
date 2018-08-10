using Dapper;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Helper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace HBSIS.GE.FileImporter.Services.Commons.Config
{
    public class ParamConfigurator : Configurator<Param>
    {
        public override Param GetCurrent()
        {
            return GetParametro();
        }

        private Param GetParametro()
        {
            Param ret = null;

            try
            {
                var sqlConn = Configuration.Actual.GetSqlConnectionString();

                using (var conn = new SqlConnection(sqlConn))
                {
                    var properties = typeof(Param).GetProperties().Select(x => x.Name).ToArray();
                    var columns = string.Join(",", properties);

                    ret = conn.Query<Param>($"SELECT TOP 1 {columns} FROM Parametro WITH(NOLOCK)").FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Ocorreu um erro ao obter o Parametro.", ex);
            }

            return ret;
        }
    }
}