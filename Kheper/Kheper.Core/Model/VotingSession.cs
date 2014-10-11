using System;
using System.Collections.Generic;

namespace Kheper.Core.Model
{
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class VotingSession
    {
        [DataMember]
        public long RoomId { get; set; }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTimeOffset VotedAt { get; set; }

        [DataMember]
        public string AgreedVote { get; set; }

        [DataMember]
        public Dictionary<long, Vote> Votes { get; set; }

        [DataMember]
        public bool IsArchived { get; set; }

        public VotingSession()
        {
            Votes = new Dictionary<long, Vote>();
        }

        public void AddOrUpdateVote(Vote vote)
        {
            if (Votes.ContainsKey(vote.Id))
            {
                Votes[vote.Id] = vote;
            }
            else
            {
                Votes.Add(vote.Id, vote);
            }
        }
    }
}