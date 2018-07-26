using HBSIS.GE.MicroserviceManagement.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.GE.MicroserviceManagement.Repository
{
    public class BaseRepository<T> where T : class, BaseModel
    {
        protected MicroserviceManagerDbContext _dbContext;
        protected MicroserviceManagerDbContext MicroserviceManagerDbContext
        {
            get
            {
                return _dbContext;
            }
        }

        protected DbSet<T> _dbSet;

        public BaseRepository()
        {
            _dbContext = new MicroserviceManagerDbContext();
            _dbSet = MicroserviceManagerDbContext.Set<T>();
        }

        public void Insert(T entityObj)
        {
            _dbSet.Add(entityObj);
            MicroserviceManagerDbContext.SaveChanges();
        }

        public void Delete(T entityObj)
        {
            if (entityObj == null || entityObj.Id == 0)
            {
                throw new Exception("Update error.");
            }

            _dbSet.Remove(entityObj);
            MicroserviceManagerDbContext.SaveChanges();
        }

        public void Update(T entityObj)
        {
            if(entityObj == null || entityObj.Id == 0)
            {
                throw new Exception("Update error.");
            }

            MicroserviceManagerDbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Where(customer => customer.Id == id).First();
        }
    }
}
