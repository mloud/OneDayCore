using System.Collections;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class JumperStateCallbacks : ABaseMono
    {
        public IEnumerator EnterBoot()
        {
            D.Info("EnterBoot");
            yield break;
        }

        public IEnumerator LeaveBoot()
        {
            D.Info("LeaveConfig");
            yield break;
        }

        public IEnumerator EnterGame()
        {
            Debug.Log("EnterGame");
            yield break;
        }

        public IEnumerator LeaveGame()
        {
            Debug.Log("LeaveGame");
            yield break;
        }
    }
}