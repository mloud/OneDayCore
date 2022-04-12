using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Core.States
{
    public class StateManager : ABaseManager
    {
        [SerializeField] StateFlow flow;
        [SerializeField] MonoBehaviour stateMethodListener;

        public State CurrentState { get; private set; }
        public bool IsTransiting { get; private set; }
        private string ActiveTrigger { get; set; }

        protected override void InternalInitialize()
        {
            Debug.Assert(!string.IsNullOrEmpty(flow.InitialState), "Initial state is empty");
            Debug.Assert(flow.GetState(flow.InitialState) != null, $"Initial state {flow.InitialState} not found");

            StartCoroutine(Trigger("start"));
        }

        public IEnumerator Trigger(string triggerName)
        {
            Debug.Assert(!IsTransiting, $"Transition in progress when trigerring {triggerName}");

            Transition transition = null;
            
            if (triggerName == "start")
            {
                transition = new Transition
                {
                    NextState = flow.InitialState,
                    Trigger = "start"
                };
            }
            else
            {
                transition = CurrentState.Transitions.Find(x => x.Trigger == triggerName);
            }
            
            if (transition != null)
            {
                IsTransiting = true;

                if (CurrentState != null)
                {
                    yield return stateMethodListener.StartCoroutine(CurrentState.OnLeaveMethodName);
                }
                
                var nextState = flow.GetState(transition.NextState);
                Debug.Assert(nextState != null, $"No such state {transition.NextState} exists");
                CurrentState = nextState;

                if (!string.IsNullOrEmpty(CurrentState.SceneToLoad))
                {
                    SceneManager.LoadScene(CurrentState.SceneToLoad);
                }

                yield return stateMethodListener.StartCoroutine(CurrentState.OnEnterMethodName);
                IsTransiting = false;

                if (!string.IsNullOrEmpty(CurrentState.OnEnterFinishedTrigger))
                {
                    ActiveTrigger = CurrentState.OnEnterFinishedTrigger;
                }
            }
            else
            {
                Debug.LogError($"No transition from current state {CurrentState} through trigger {triggerName}");
            }
        }

        private void Update()
        {
            if (ActiveTrigger != null)
            {
                StartCoroutine(Trigger(ActiveTrigger));
                ActiveTrigger = null;
            }
        }
    }
}