﻿using HBSIS.Framework.Commons.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using System.Data.SqlClient;
using HBSIS.Framework.Commons.Utils;
using HBSIS.Framework.Commons.Data;
using Dapper.Contrib.Extensions;

namespace HBSIS.Framework.Data.Dapper
{
    public class DapperRepository<TEntity, TId> : Disposable, IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        public DapperRepository(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        private string _connectionString;

        protected DapperDataContext DataContext { get; private set; }

        public Type ElementType { get { return Query.ElementType; } }

        public Expression Expression { get { return Query.Expression; } }

        public IQueryProvider Provider { get { return Query.Provider; } }

        public IEnumerable<TEntity> Collection
        {
            get { return new List<TEntity>(); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IQueryable<TEntity> Query
        {
            get { return Collection.AsQueryable(); }
        }

        public IQueryable<TEntity> GetQuery(IFetchStrategy<TEntity, TId> fetchStrategy)
        {
            return Query;
        }

        public IEnumerable<TEntity> GetAll(string tableName)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();
                return dapperConnection.Query<TEntity>($"SELECT * FROM OPMDM.{tableName}");
            }
        }

        public void ExecuteCommandDefinition(List<CommandDefinition> commands)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                foreach (var command in commands)
                    dapperConnection.Execute(command);
            }
        }

        public IDbConnection AbreConexao()
        {
            return new SqlConnection(_connectionString);
        }

        public void Insert(TEntity entity)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                try
                {
                    dapperConnection.Insert<TEntity>(entity);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}