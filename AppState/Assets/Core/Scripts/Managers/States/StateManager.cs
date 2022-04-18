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

            StartCoroutine(TriggerCoroutine("start"));
        }

        public void Trigger(string triggerName)
        {
            StartCoroutine(TriggerCoroutine(triggerName));
        }
        
        private IEnumerator TriggerCoroutine(string triggerName)
        {
            Debug.Assert(!IsTransiting, $"Transition in progress when triggering {triggerName}");
            
            D.Info($"Triggering trigger {triggerName}");
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
                    if (stateMethodListener != null)
                    {
                        D.Info($"Starting leave method in state {CurrentState.Name}");
                        yield return stateMethodListener.StartCoroutine(CurrentState.OnLeaveMethodName);
                    }
                }
                
                var nextState = flow.GetState(transition.NextState);
                Debug.Assert(nextState != null, $"No such state {transition.NextState} exists");
                CurrentState = nextState;

                if (!string.IsNullOrEmpty(CurrentState.SceneToLoad))
                {
                    D.Info($"Loading scene {CurrentState.SceneToLoad} when entering state {CurrentState.Name}");
                    SceneManager.LoadScene(CurrentState.SceneToLoad);
                }

                if (stateMethodListener != null)
                {
                    D.Info($"Starting enter method in state {CurrentState.Name}");
                    yield return stateMethodListener.StartCoroutine(CurrentState.OnEnterMethodName);
                }

                IsTransiting = false;

                if (!string.IsNullOrEmpty(CurrentState.OnEnterFinishedTrigger))
                {
                    D.Info($"Assigning automatic leave trigger when finishing enter method in state {CurrentState.Name}");
                    ActiveTrigger = CurrentState.OnEnterFinishedTrigger;
                }
            }
            else
            {
                D.Error($"No transition from current state {CurrentState} through trigger {triggerName}");
            }
        }

        private void Update()
        {
            if (ActiveTrigger != null)
            {
                D.Info($"New trigger {ActiveTrigger} detected - starting Trigger new state sequence");
                StartCoroutine(TriggerCoroutine(ActiveTrigger));
                ActiveTrigger = null;
            }
        }
    }
}