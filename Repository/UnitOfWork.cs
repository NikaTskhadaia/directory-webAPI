using AutoMapper;
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
        private readonly IMapper _mapper;

        public UnitOfWork(PeopleDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            People = new PersonRepository(_db, _mapper);
        }

        public IPersonRepository People { get; private set; }

        public int Save()
        {
            return _db.SaveChanges();
        }
    }
}
