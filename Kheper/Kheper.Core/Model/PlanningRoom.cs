using System.Collections.Generic;

namespace Kheper.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class PlanningRoom
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public Dictionary<long, VotingSession> Sessions;

        [DataMember]
        public List<string> Users;

        [DataMember]
        public bool IsArchived { get; set; }

        public PlanningRoom()
        {
            Sessions = new Dictionary<long, VotingSession>();
            Users = new List<string>();
        }

        public void AddOrUpdateSession(VotingSession session)
        {
            if (Sessions.ContainsKey(session.Id))
            {
                this.Sessions[session.Id] = session;
            }
            else
            {
                this.Sessions.Add(session.Id, session);
            }
        }
    }
}
