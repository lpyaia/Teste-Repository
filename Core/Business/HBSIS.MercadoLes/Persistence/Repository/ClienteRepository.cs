using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Persistence.IRepository;

namespace HBSIS.MercadoLes.Persistence.Repository
{
    public class ClienteRepository : DapperRepository<Cliente, Guid>, IClienteRepository<Cliente>
    {

        public ClienteRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Cliente> GetAll()
        {
            return base.GetAll("TB_CLIENTE");
        }

        public Cliente Get(long cdCliente)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<Cliente>("SELECT * FROM OPMDM.TB_CLIENTE WHERE CdCliente = @CdCliente", 
                    new { CdCliente = cdCliente }).FirstOrDefault();
            }
        }
    }
}
