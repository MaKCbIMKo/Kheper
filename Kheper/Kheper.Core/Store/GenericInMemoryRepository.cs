using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Kheper.Core.Store
{
	public class GenericInMemoryRepository<TEntity,TId> : IRepository<TEntity, TId> where TEntity : class
	{
		private readonly ConcurrentDictionary<string, TEntity> _store = new ConcurrentDictionary<string, TEntity>();

		protected string GenerateKey(TId id)
		{
			return id.ToString();
		}

		public TEntity Query(TId id)
		{
			var key = GenerateKey(id);
			TEntity instance;
			if (_store.TryGetValue(key, out instance))
			{
				return instance;
			}
			else
			{
				throw new StoreException("Store does not contain instance of " + typeof(TEntity).Name + " with key " + key);
			}
		}

		public IQueryable<TEntity> Query(Func<TEntity, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public TEntity Save(TEntity instance)
		{
			throw new NotImplementedException();
		}

		public void Delete(TId id)
		{
			throw new NotImplementedException();
		}
	}
}
