using System;
using System.Collections.Generic;

namespace Kheper.Core.Model
{
    [Serializable]
    public class VotingSession
	{
		public string Description { get; set; }
		public DateTimeOffset VotedAt { get; set; }
		public int? AgreedVote { get; set; }
		public List<Vote> Votes { get; set; }
	}
}