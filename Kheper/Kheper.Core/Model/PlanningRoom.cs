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
        public List<VotingSession> Sessions;

        [DataMember]
        public List<string> Users;

        public PlanningRoom()
        {
            Sessions = new List<VotingSession>();
            Users = new List<string>();
        }
    }
}
