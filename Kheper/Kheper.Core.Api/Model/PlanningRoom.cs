using System.Collections.Generic;

namespace Kheper.Core.Model
{
	public class PlanningRoom
	{
		public long Id { get; set; }

		public List<VotingSession> Sessions;

		public List<string> Users;
	}
}
