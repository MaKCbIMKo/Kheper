using System.Linq;

namespace Kheper.Core.Store
{
	public interface IRepository<TEntity, in TId> where TEntity : class
	{
		TEntity Query(TId id);
		IQueryable<TEntity> Query();
		TEntity Save(TEntity instance);
		void Delete(TEntity instance);
	}
}
