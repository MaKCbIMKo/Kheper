using System;

namespace Kheper.Core.Store
{
	public class StoreException : Exception
	{
		public StoreException(string message)
			: base(message)
		{
		}
	}
}