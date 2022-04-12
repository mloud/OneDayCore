using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.States
{
    [CreateAssetMenu(fileName = "StateFlow", menuName = "OneDay/States/Flow", order = 1)]
    public class StateFlow : ScriptableObject
    {
        public string InitialState;
        public List<State> States;
        public State GetState(string name) => States.Find(x => x.Name == name);
    }
}