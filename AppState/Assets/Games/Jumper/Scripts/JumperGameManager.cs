using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using OneDay.Core;
using OneDay.Core.States;
using OneDay.Games.Jumper.Ui;
using UnityEngine;


namespace OneDay.Games.Jumper
{
    public class JumperGameManager : ABaseManager
    {
        [SerializeField] private JumperGameUiPanel ui;
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private CinemachineVirtualCamera winCam;
        [SerializeField] private CinemachineVirtualCamera startCam;
        
        private enum State
        {
            WarmingUp,
            Running,
            LevelWon,
            LevelFailed
        }

        private State state;
        protected override void InternalInitialize()
        {
            SwitchToState(State.WarmingUp);
        }

        private CatchTrigger catcher;
        private SimpleCharacter player;
        private Level level;
        
        private IEnumerator DoWarmUp()
        {
            state = State.WarmingUp;
            
            startCam.enabled = true;
            // find level
            level = GameObject.FindObjectOfType<Level>();
            
            // find player
            player = GameObject.FindObjectOfType<SimpleCharacter>();
            var playerInput = player.GetComponent<PlayerInput>();
            playerInput.InputType = PlayerInput.Type.Manual;
            playerInput.MoveValue = 0;
            player.OnFinished += OnPlayerFinished;
            player.OnFailed += OnPlayerKilled;
            
            // catcher
            catcher = GameObject.FindObjectOfType<CatchTrigger>();
            catcher.MaxSpeed = level.LevelSettings.Speed;
            catcher.Speed = 0;
            
            ui.ProgressPanel.Set(0,0);
            
            
            // wait
            yield return new WaitForSeconds(0.5f);
            startCam.enabled = false;
            yield return new WaitForSeconds(1.5f);
            
            
            // start running player
            DOTween.To(() => playerInput.MoveValue, (value) => playerInput.MoveValue = value, 1.0f, 1.0f)
                .SetEase(Ease.OutExpo);
            
            // start running catcher
            DOTween.To(() => catcher.Speed, (value) => catcher.Speed = value, catcher.MaxSpeed, 1.5f)
                .SetEase(Ease.OutExpo).SetDelay(0.2f);

            SwitchToState(State.Running);
        }

        private IEnumerator DoRunning()
        {
            while (state == State.Running)
            {
                ui.ProgressPanel.Set(
                    player.transform.position.x / level.LevelSettings.Length,
                    catcher.transform.position.x / level.LevelSettings.Length);
                yield return 0;
            }
        }
        
        private void SwitchToState(State state)
        {
            this.state = state;
            switch (state)
            {
                case State.WarmingUp:
                    StartCoroutine(DoWarmUp());
                    break;
                case State.Running:
                    StartCoroutine(DoRunning());
                    break;    
                case State.LevelWon:
                    StartCoroutine(DoLevelWon());
                    break;
                case State.LevelFailed:
                    StartCoroutine(DoLevelFailed());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnPlayerFinished()
        {
            SwitchToState(State.LevelWon);
        }

        private void OnPlayerKilled()
        {
            SwitchToState(State.LevelFailed);
        }

        private IEnumerator DoLevelWon()
        {
            winCam.enabled = true;
            yield return catcher.Vanish();
            yield return new WaitForSeconds(1.0f);

            ODApp.Instance.ManagerHub.Get<StateManager>().Trigger("StartGame");
        }
        
        private IEnumerator DoLevelFailed()
        {
            yield return new WaitForSeconds(1.0f);
            var transposer = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            yield return DOTween
                .To(() => transposer.m_ScreenX, (v) => transposer.m_ScreenX = v, 0.5f, 2.0f)
                .WaitForCompletion();
            yield return new WaitForSeconds(1.0f);
            ODApp.Instance.ManagerHub.Get<StateManager>().Trigger("StartGame");
        }
    }
}