using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public void Add(T entity);

        T Get(int entityId);

        void Update(T entity);

        IEnumerable<T> GetAll(string searchCriteria, int numberOfObjectsPerPage, int pageNumber);

        void Remove(int personId);
    }
}
