using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Ui
{
    public class UiFactory : ABaseMono
    {
        [SerializeField] private UiFactoryDatabase factoryDatabase;
        
        public List<T> CreateWidgets<T>(int count, Transform parent = null) where T : ABaseWidget
        {
            var widgets = new List<T>(count);
           
            foreach(Transform tr in parent)
            {
                T existingWidget = tr.GetComponent<T>();
                if (existingWidget != null)
                {
                    widgets.Add(existingWidget);
                }
            }
            int widgetNeedsToCreate = Mathf.Max(0, count - widgets.Count);
            for (int i = 0; i < widgetNeedsToCreate; i++)
            {
                widgets.Add(CreateWidget<T>(parent));
            }

            for (int i = 0; i < widgets.Count; i++)
            {
                widgets[i].gameObject.SetActive(i < count);
            }
            
            return widgets;
        }
        
        private T CreateWidget<T>(Transform parent = null) where T: ABaseWidget
        {
            var widgetPrefab = factoryDatabase.WidgetPrefabs.Find(x => x.GetType() == typeof(T));
            D.Assert(widgetPrefab != null, $"No such widget prefab of type {typeof(T)} registered in {factoryDatabase.name} database", factoryDatabase);
            return (T)Instantiate(widgetPrefab, parent);
        }
    }
}