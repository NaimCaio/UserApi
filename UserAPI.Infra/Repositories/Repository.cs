
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Domain.Interfaces;
using UserAPI.Infra.EF;

namespace UserAPI.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UserAPIDBContext _dbcontext;
        private DbSet<T> table;
        public Repository(UserAPIDBContext dBContext)
        {
            _dbcontext = dBContext;
            table = _dbcontext.Set<T>();
        }

        public T Find(int id)
        {
            return table.Find(id);
        }

        public IQueryable<T> List()
        {
            return table;
        }

        public void Add(T item)
        {
            table.Add(item);
        }

        public void Remove(T item)
        {
            table.Remove(item);
        }

        public void Edit(T item)
        {
            _dbcontext.Entry(item).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
        public void Save()
        {
            _dbcontext.SaveChanges();
        }

        public T FirstOrDeafault(Expression<Func<T, bool>> clause)
        {
            return table.FirstOrDefault(clause);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> clause)
        {
            return table.Where(clause);
        }
    }
}
