using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Kheper.Web.Hubs
{
	public class BaseHub : Hub
	{
		public override Task OnConnected()
		{
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected()
		{
			return base.OnReconnected();
		}

		protected Result Result(bool isSuccess = true, string errorMessage = null)
		{
			return new Result
			{
				IsSuccess = isSuccess,
				ErrorMEssage = errorMessage
			};
		}

		protected ObjectResult<T> Result<T>(T result, bool isSuccess = true, string errorMessage = null)
		{
			return new ObjectResult<T>
			{
				IsSuccess = isSuccess,
				ErrorMEssage = errorMessage,
				Result = result
			};
		}
	}

	public class Result
	{
		public bool IsSuccess { get; set; }

		public string ErrorMEssage { get; set; }
	}

	public class ObjectResult<T> : Result
	{
		public T Result { get; set; }
	}
}