using System;
using System.Collections.Generic;
using System.Linq;
using RulesRepository.ExtensionMethods;
using RulesRepository.UnitOfWorkInterfaces;
using System.Data.Objects;
using System.Linq.Expressions;
using RuleEngine.RepositoryInterfaces;

namespace RulesRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ObjectContext _context;
        private IObjectSet<T> _objectSet;
        private readonly IEfUnitOfWork<ObjectContext> _unitOfWork;

        #region Constructor

        public GenericRepository(IUnitOfWork uoW)
        {
            if (uoW == null)
                throw new ArgumentNullException("uoW", "GenericRepository: Unit of Work not Provided");
            _unitOfWork = (IEfUnitOfWork<ObjectContext>)uoW;
            _context = _unitOfWork.GetContext;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Returns an IQueryable object containing all the objects in the specified entity type.
        /// Used in cases to build customized queries against the entity.
        /// </summary>
        /// <returns>IQueryable object</returns>
        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        /// <summary>
        /// Returns an object of type IEnumerable for all the entity objects. 
        /// Sorting is provided by entering an optional lambda expression. 
        /// Example of use: GetAll(q => q.OrderBy(x => x.FirstName))
        /// </summary>
        /// <returns>IEnumerable list</returns>
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return orderBy == null ? ObjectSet.ToList() : orderBy(ObjectSet).ToList();
        }

        /// <summary>
        /// Executes the 'Where' method for the provided lambda expression. If 
        /// records not found, returns object with count property set to zero.
        /// </summary>
        /// <param name="whereFilter">Lambda expression</param>
        /// <param name="include">String[] of aggregate objects to be included.</param>
        /// <returns>Returns IEnumerable object. If no objects found, returns object 
        /// with count set to zero.</returns>
        public IList<T> Find(Expression<Func<T, bool>> whereFilter, string[] include)
        {
            return include != null
                ? ObjectSet.Include<T>(include).Where(whereFilter).ToList()
                : ObjectSet.Where(whereFilter).ToList();
        }

        /// <summary>
        /// Use in situations where one and only one element should satisfy the 
        /// query condition. If no objects are found null is returned. If more 
        /// than one object meets the query conditions an exception is thrown.
        /// </summary>
        /// <param name="where">Lambda expression</param>
        /// <param name="include">String[] of aggregate objects to be included.</param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> where, string[] include)
        {
            return (include != null)
                ? ObjectSet.Include<T>(include).SingleOrDefault(where)
                : ObjectSet.SingleOrDefault(where);
        }

        /// <summary>
        /// Returns the first element in a sequence that meets the specified 
        /// conditions or return null if no objects are found.
        /// </summary>
        /// <param name="where">Lambda expression</param>
        /// <param name="include">String[] of aggregate objects to be included.</param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> where, string[] include)
        {
            return (include != null)
                ? ObjectSet.Include<T>(include).FirstOrDefault(where)
                : ObjectSet.FirstOrDefault(where);
        }

        /// <summary>
        /// Add a newly created entity to the context.
        /// </summary>
        /// <param name="entity">Entity being added to context.</param>
        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "An Entity not provided for addition.");
            ObjectSet.AddObject(entity);
        }

        /// <summary>
        /// Mark entity for deletion in context.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "An Entity not provided for deletion.");
            ObjectSet.DeleteObject(entity);
        }

        /// <summary>
        /// Save changes to the context. Use Unit of Work when possible.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion
        #region Private Methods

        private IObjectSet<T> ObjectSet
        {
            get { return _objectSet ?? (_objectSet = _context.CreateObjectSet<T>()); }
        }

        #endregion
    }
}

