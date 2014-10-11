using System;

namespace Kheper.Core.Api.Store
{
	public class StoreException : Exception
	{
		public StoreException(string message)
			: base(message)
		{
		}
	}
}