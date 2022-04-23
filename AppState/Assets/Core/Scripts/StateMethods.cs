using OneDay.Core;
using OneDay.Core.Ui;
using System.Collections;
using OneDay.Core.Modules;
using UnityEngine;

namespace OneDay.Example
{
    public class StateMethods : ABaseMono
    {
        public IEnumerator EnterConfig()
        {
            // configure app gere
            ODApp.Instance.ModuleHub.Register("user", new ModuleRegister().Register<IValletModule>(new ValletModule(new ValletData())));
            Debug.Log("EnterConfig");
            yield break;
        }

        public IEnumerator LeaveConfig()
        {
            Debug.Log("LeaveConfig");
            yield break;
        }

        public IEnumerator EnterBoot()
        {
            Debug.Log("EnterBoot");
            yield return new WaitForSeconds(1.0f);
        }

        public IEnumerator LeaveBoot()
        {
            Debug.Log("LeaveBoot");
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

        public IEnumerator EnterMenu()
        {
            Debug.Log("EnterMenu");

            StartCoroutine(ODApp.Instance.ManagerHub.Get<UiManager>().Show("ConfirmDialog", KeyValueData.Create().Add("text", "Test label")));

            yield break;
        }

        public IEnumerator LeaveMenu()
        {
            Debug.Log("LeaveMenu");
            yield break;
        }
    }
}
