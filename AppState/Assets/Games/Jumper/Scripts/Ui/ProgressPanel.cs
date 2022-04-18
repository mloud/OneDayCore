using OneDay.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Games.Jumper.Ui
{
    public class ProgressPanel : UiController
    {
        [SerializeField] private Slider playerProgress;
        [SerializeField] private Slider enemyProgress;

        public void Set(float playerProgress01, float enemyProgress01)
        {
            playerProgress.value = playerProgress01;
            enemyProgress.value = enemyProgress01;
        }
    }
}