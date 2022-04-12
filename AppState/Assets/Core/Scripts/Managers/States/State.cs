using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Core.States
{
    [CreateAssetMenu(fileName = "State", menuName = "OneDay/States/State", order = 1)] 
    public class State : ScriptableObject
    {
        public string Name;
        public string SceneToLoad;
        public string OnEnterMethodName;
        public string OnLeaveMethodName;
        public string OnEnterFinishedTrigger; 
        public List<Transition> Transitions;
    }
}
