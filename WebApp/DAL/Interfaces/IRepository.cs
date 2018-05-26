using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        IEnumerable<T> Find(Func<T, Boolean> predicate); //Encapsulates a method that has one parameter and returns a value of the type specified by second parameter.
        void Create(T item);
        void Update(T item);
        void Delete(int Id);
    }
}
