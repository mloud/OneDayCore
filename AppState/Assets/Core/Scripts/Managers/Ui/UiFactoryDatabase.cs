using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Ui
{
    [CreateAssetMenu(fileName = "UiFactoryDatabase", menuName = "OneDay/Ui/UiFactoryDatabase", order = 1)]
    public class UiFactoryDatabase : ScriptableObject
    {
        public List<ABaseWidget> WidgetPrefabs;
    }
}