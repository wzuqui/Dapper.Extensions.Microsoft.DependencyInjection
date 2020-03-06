using System.Collections.Generic;
using Dapper.Contrib.Extensions;

// ReSharper disable InconsistentNaming

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public abstract class DapperDbSet
    {
    }

    public class DapperDbSet<T> : DapperDbSet where T : class
    {
        private readonly DapperContext _context;

        public DapperDbSet(DapperContext pContext)
        {
            _context = pContext;
        }

        public T Get(int id) => _context.Connection.Get<T>(id);
        public IEnumerable<T> GetAll() => _context.Connection.GetAll<T>();
        public long Insert(T obj) => _context.Connection.Insert(obj);
        public long Insert(IEnumerable<T> list) => _context.Connection.Insert(list);
        public bool Update(T obj) => _context.Connection.Update(obj);
        public bool Update(IEnumerable<T> list) => _context.Connection.Update(list);
        public bool Delete(T obj) => _context.Connection.Delete(obj);
        public bool Delete(IEnumerable<T> list) => _context.Connection.Delete(list);
        public bool DeleteAll() => _context.Connection.DeleteAll<T>();
    }
}