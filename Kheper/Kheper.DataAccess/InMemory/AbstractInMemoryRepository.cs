using System;
using System.Collections.Concurrent;
using System.Linq;
using Kheper.Core.Store;

namespace Kheper.DataAccess.InMemory
{
    using Kheper.Core.Util;

    public abstract class AbstractInMemoryRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class where TId : struct
    {
        private readonly ConcurrentDictionary<TId, TEntity> _store = new ConcurrentDictionary<TId, TEntity>();

        protected abstract TId? GetId(TEntity instance);
        protected abstract void SetId(TEntity instance, TId id);
        protected abstract TId GenerateId(TEntity instance);

        protected TEntity Clone(TEntity instance)
        {
            return Serialization.Clone(instance);
        }

        public virtual TEntity Query(TId id)
        {
            TEntity instance;
            if (!this._store.TryGetValue(id, out instance))
            {
                ThrowInstanceNotFound(id);
            }

            return this.Clone(instance);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _store.Values.Select(Clone).AsQueryable();
        }

        public virtual TEntity Save(TEntity instance)
        {
            instance = this.Clone(instance);
            TId? id = GetId(instance);
            if (!id.HasValue)
            {
                id = GenerateId(instance);
                SetId(instance, id.Value);
            }

            return Clone(_store.AddOrUpdate(id.Value, instance, (k, i) => i));
        }

        public virtual void Delete(TEntity instance)
        {
            var id = GetId(instance);
            if (!id.HasValue)
            {
                throw new StoreException("Cannot delete instance because it has empty identity");
            }

            this._store.TryRemove(id.Value, out instance);
        }

        protected static void ThrowInstanceNotFound(TId id)
        {
            throw new StoreException("Store does not contain instance of " + typeof(TEntity).Name + " with key " + id);
        }
    }
}
