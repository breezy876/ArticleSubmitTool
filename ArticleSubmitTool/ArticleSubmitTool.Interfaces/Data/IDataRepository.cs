using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain.Interfaces;

namespace ArticleSubmitTool.Interfaces.Data
{
    //TODO
    public interface IDataRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(IEnumerable<long> ids);

        T Get(long id);

        IQueryable<T> Query(Func<T, bool> cond);

        IEnumerable<long> AddOrUpdate(IEnumerable<IDataEntity> items);
        long AddOrUpdate(IDataEntity item);

        //void Remove(Func<T, bool> cond);
        //void RemoveAll();

        void Remove(long id);
        void Remove(IEnumerable<long> ids);
    }
}
