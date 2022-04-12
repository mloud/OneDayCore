using System;


namespace OneDay.Core.States
{
    [Serializable]
    public class Transition
    {
        public string Trigger;
        public string NextState;
    }
}
