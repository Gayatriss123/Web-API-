using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

//This is a Generic Repository of CRUD operation in EF.
//It is Generic for all entities in the project
//The benefit of this generic repository is it saves time for writing the CRUD operations for each entity seperately
//Also it helps to maximize code reuse, type safety, and performance

namespace CustomerServiceWebAPI.Generic_Repository
{
    public class GenericRepositoryEF<T> : IGenericRepositoryEF<T> where T : class
    {
        private Intern_DBEntities _context = null;
        private DbSet<T> table = null;
        public GenericRepositoryEF()
        {
            this._context = new Intern_DBEntities();
            table = _context.Set<T>();
        }
        public GenericRepositoryEF(Intern_DBEntities _context)
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
            _context.SaveChanges();
        }
    }
}