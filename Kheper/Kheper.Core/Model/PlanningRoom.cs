using System;
using System.Collections.Generic;

namespace Kheper.Core.Model
{
	public class PlanningRoom
	{
		public Guid Id { get; set; }

		public List<VotingSession> Sessions;

		public List<string> Users;
	}
}
