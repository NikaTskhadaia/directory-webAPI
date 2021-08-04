using PersonDirectory.Domain;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Persistence.Data;
using PersonDirectory.Persistence.Models;
using PersonDirectory.Persistence.Repository;

namespace PersonDirectory.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PeopleDb _db;

        public UnitOfWork(PeopleDb db)
        {
            _db = db;
            People = new PersonRepository(_db);
        }

        public IPersonRepository People { get; private set; }

        public int Save()
        {
            return _db.SaveChanges();
        }
    }
}
