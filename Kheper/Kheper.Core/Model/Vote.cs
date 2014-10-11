namespace Kheper.Core.Model
{
    using System;

    [Serializable]
    public class Vote
    {
        public Vote(string userName, int value)
        {
            UserName = userName;
            Value = value;
        }

        public string UserName { get; set; }
        public int Value { get; set; }
    }
}