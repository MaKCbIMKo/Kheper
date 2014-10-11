using System;
using System.Collections.Concurrent;
using System.Linq;
using Kheper.Core.Api.Store;

namespace Kheper.Core.Store
{
	public abstract class GenericInMemoryRepository<TEntity,TId> : IRepository<TEntity, TId> where TEntity : class
	{
		private readonly ConcurrentDictionary<string, TEntity> _store = new ConcurrentDictionary<string, TEntity>();

		protected string GenerateKey(TId id)
		{
			return id.ToString();
		}

		protected abstract TId GetId(TEntity instance);

		protected TEntity Clone(TEntity instance)
		{
			throw new NotImplementedException();
		}

		public TEntity Query(TId id)
		{
			var key = GenerateKey(id);
			TEntity instance;
			if (_store.TryGetValue(key, out instance))
			{
				return Clone(instance);
			}
			else
			{
				throw new StoreException("Store does not contain instance of " + typeof(TEntity).Name + " with key " + key);
			}
		}

		public IQueryable<TEntity> Query()
		{
			return _store.Values.Select(Clone).AsQueryable();
		}

		public TEntity Save(TEntity instance)
		{
			string key = GenerateKey(GetId(instance));
			return Clone(_store.AddOrUpdate(key, instance, (k, i) => i));
		}

		public void Delete(TEntity instance)
		{
			var id = GetId(instance);
			var key = GenerateKey(id);
			_store.TryRemove(key, out instance);
		}
	}
}
