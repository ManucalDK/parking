using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(string id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
