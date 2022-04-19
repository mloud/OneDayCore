using System.Collections;
using OneDay.Core.Effects;
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
        private string LoadingScene { get; set; }
        
        protected override void InternalInitialize()
        {
            Debug.Assert(!string.IsNullOrEmpty(flow.InitialState), "Initial state is empty");
            Debug.Assert(flow.GetState(flow.InitialState) != null, $"Initial state {flow.InitialState} not found");

            SceneManager.sceneLoaded += OnSceneLoaded;
            
            StartCoroutine(TriggerCoroutine("start"));
        }

        protected override void InternalRelease()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
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

                        if (!string.IsNullOrEmpty(CurrentState.OnLeaveTransition))
                        {
                            yield return ODApp.Instance.ManagerHub.Get<TransitionManager>().Show(CurrentState.OnLeaveTransition);
                        }
                    }
                }
                
                var nextState = flow.GetState(transition.NextState);
                Debug.Assert(nextState != null, $"No such state {transition.NextState} exists");
                CurrentState = nextState;

                 
                if (!string.IsNullOrEmpty(CurrentState.SceneToLoad))
                {
                    D.Info($"Loading scene {CurrentState.SceneToLoad} when entering state {CurrentState.Name}");
                    LoadingScene = CurrentState.SceneToLoad;
                    SceneManager.LoadScene(CurrentState.SceneToLoad);

                    yield return new WaitUntil(() => LoadingScene == null);
                }

                yield return new WaitForEndOfFrame();
                if (stateMethodListener != null)
                {
                    D.Info($"Starting enter method in state {CurrentState.Name}");
                    yield return stateMethodListener.StartCoroutine(CurrentState.OnEnterMethodName);
                }

                yield return ODApp.Instance.ManagerHub.Get<TransitionManager>().Hide();

                
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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!string.IsNullOrEmpty(LoadingScene))
            {
                Debug.Assert(scene.name == LoadingScene, $"Expected scene {LoadingScene} but scene loaded is {scene.name}");
                LoadingScene = null;
            }
        }
        private void Update()
        {
            if (ActiveTrigger != null && string.IsNullOrEmpty(LoadingScene))
            {
                D.Info($"New trigger {ActiveTrigger} detected - starting Trigger new state sequence");
                StartCoroutine(TriggerCoroutine(ActiveTrigger));
                ActiveTrigger = null;
            }
        }
    }
}