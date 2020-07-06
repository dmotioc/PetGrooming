using PetGroomingApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.GenericRepository
{
    public enum DbError
    {
        UniqueConstraint = 2627,
        ConstraintCheckViolation = 547,
        DuplicateKey = 2601
    }


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        private GroomingContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new GroomingContext();
            table = _context.Set<T>();
        }

        public GenericRepository(GroomingContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                if (e.GetBaseException().GetType() == typeof(SqlException))
                {
                    SqlException eBase = (SqlException)e.GetBaseException();
                    Int32 errorCode = eBase.Number;
                    if (Enum.IsDefined(typeof(DbError), errorCode))
                    {
                        throw new Exception(errorCode.ToString());
                    }
                    else
                    {
                        throw new Exception("Error: " + errorCode.ToString()  + ", " + e.Message);
                    }
                }
                else
                {
                    throw new Exception(e.Message);
                }

            }
        }
    }
}