namespace Kheper.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class Vote
    {
        [DataMember]
        public long PlanningRoomId { get; set; }

        [DataMember]
        public long VotingSessionId { get; set; }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}