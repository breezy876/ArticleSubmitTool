using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Domain.Interfaces;
using ArticleSubmitTool.Interfaces.Data;
using ArticleSubmitTool.Shared;

namespace ArticleSubmitTool.Code
{


    public class EFDataRepository<T> : IDataRepository<T> where T : class, IDataEntity
    {

        public User User { get; set; }

        public bool ExcludeDeleted { get; set; }
        public bool TrackChanges { get; set; }
        public bool SaveChanges { get; set; }
        public bool DeletePermanent { get; set; }
        public bool IncludeRelated { get; set; }
        public bool LimitTotal { get; set; }

        public long StartIndex { get; set; }
        public long TotalRows { get; set; }

        public IEnumerable<object> NavigationProperties { get; set; }

        private DataContext _context;

        private Type _type;

        public EFDataRepository(DataContext context) : this()
        {
            _context = context;
        }

        public EFDataRepository()
        {
            IncludeRelated = true;
            SaveChanges = true;
            DeletePermanent = false;

            _type = typeof(T);
        }

        #region private methods
        private bool _QueryCondition(IDataEntity item)
        {
            return (User == null || (item as ITrackable).CreatedBy == User.Id) &&
                        (!ExcludeDeleted || !(item as IDeletable).IsDeleted);
        }

        private IQueryable<T> _GetQueryable(IQueryable<T> unFiltered = null)
        {

            IQueryable<T> qResult = unFiltered.IsNullOrEmpty() ? _context.Set<T>().AsQueryable() : unFiltered;

            if (User != null)
            {
                if (typeof(IQueryable<ITrackable>).IsAssignableFrom(qResult.GetType()))
                {
                    qResult = (qResult as IQueryable<ITrackable>).Where(q => q.CreatedBy == User.Id).Cast<T>();
                }
            }

            if (ExcludeDeleted)
            {
                if (typeof(IQueryable<IDeletable>).IsAssignableFrom(qResult.GetType()))
                {
                    qResult = (qResult as IQueryable<IDeletable>).Where(q => !q.IsDeleted).Cast<T>();
                }
            }

            if (IncludeRelated && !NavigationProperties.IsNullOrEmpty())
            {

                foreach (var navProp in NavigationProperties)
                {
                    qResult = qResult.Include(navProp as Expression<Func<T, object>>);
                }
            }

            return qResult;
        }

        private IQueryable<T> _GetResults(IQueryable<T> qInput)
        {
            if (LimitTotal)
            {
                var qResult = qInput as IQueryable<IDataEntity>;
                return qResult.OrderBy(q => q.Id).Skip((int)StartIndex).Take((int)TotalRows).Cast<T>();
            }
            return qInput;
        }
        #endregion

        #region static methods

        #endregion


        #region public methods

        #region read methods
        public IQueryable<T> GetAll()
        {
            return _GetResults(_GetQueryable());
        }

        public T Get(long id)
        {
            return _GetQueryable().FirstOrDefault(x => x.Id.Equals(id));
        }

        public IQueryable<T> Get(IEnumerable<long> ids)
        {
            return _GetResults(_GetQueryable().Where(x => ids.Contains(x.Id)));
        }

        public IQueryable<T> Query(Func<T, bool> cond)
        {
            return _GetResults(_GetQueryable().Where(cond).AsQueryable());
        }
        #endregion


        #region write methods
        public IEnumerable<long> AddOrUpdate(IEnumerable<IDataEntity> items)
        {
            var ids = new List<long>();
            foreach (var item in items)
            {
                var newId = AddOrUpdate(item);
                ids.Add(newId);
            }
            return ids;
        }

        public long AddOrUpdate(IDataEntity item)
        {
            long newId;
            if (((long)item.Id) == 0)
            {
                newId = _Add(item);
            }
            else
            {
                var existingItem = (IDataEntity)_context.Set(_type).Find(item.Id);
                if (existingItem != null)
                {
                    newId = _Update(existingItem, item);
                }
                else
                {
                    newId = _Add(item);
                }
            }
            if (SaveChanges)
            {
                _context.SaveChanges();
            }
            return newId;
        }

        private long _Add(IDataEntity item)
        {

            var set = _context.Set(_type);

            IDataEntity newItem;

            if ((typeof (IDeletable).IsAssignableFrom(item.GetType())))
            {
                var delItem = (IDeletable) item;
                delItem.IsDeleted = false;
            }

            if (TrackChanges)
            {
                var trackItem = (ITrackable)item;
                trackItem.DateCreated = DateTime.Now;
                if (User != null)
                {
                    trackItem.CreatedBy = User.Id;
                    trackItem.CreatedByUser = User;
                }
                newItem = (IDataEntity)set.Add(trackItem);
            }
            else
            {
                newItem = (IDataEntity)set.Add(item);
            }
            return newItem.Id;
        }

        private long _Update(IDataEntity existingItem, IDataEntity item)
        {
            if (TrackChanges)
            {
                var trackItem = (ITrackable)item;
                trackItem.DateModified = DateTime.Now;
                if (User != null)
                {
                    trackItem.ModifiedBy = User.Id;
                    trackItem.ModifiedByUser = User;
                }
                item = (IDataEntity)trackItem;
            }

            var props = _type.GetProperties();

            foreach (var prop in props)
            {
                var newVal = prop.GetValue(item, null);
                if (newVal != null)
                {
                    prop.SetValue(existingItem, newVal, null);
                }
            }

            return item.Id;

        }

        public void Remove(long id)
        {

            var set = _context.Set(_type);

            var existingItem = set.Find(id);

            if (existingItem != null)
            {
                if (DeletePermanent)
                {
                    set.Remove(existingItem);
                }
                else
                {
                    var delItem = (IDeletable)existingItem;
                    delItem.IsDeleted = true;

                    if (TrackChanges)
                    {
                        delItem.DateDeleted = DateTime.Now;
                        if (User != null)
                        {
                            delItem.DeletedBy = User.Id;
                        }
                    }
                    AddOrUpdate((IDataEntity)delItem);
                }

            }
            if (SaveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Remove(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                Remove(id);
            }
        }
        #endregion

        #endregion


    }
}
