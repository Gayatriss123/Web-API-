using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is a interface of Generic Repository of CRUD operation in EF.

namespace CustomerServiceWebAPI.Generic_Repository
{
    public interface IGenericRepositoryEF<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
