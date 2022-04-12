using System.Reflection;
using UnityEngine;

namespace OneDay.Core.Inject
{
    public static class ModuleInjector
    {
        public static void TryToInject(MonoBehaviour monoBehaviour)
        {
            var properties = Utils.ListOfPropertiesFromInstance(monoBehaviour);

            foreach (var property in properties)
            {
                var injectModuleAttribute = property.GetCustomAttribute<InjectModuleAttribute>();
                if (injectModuleAttribute != null)
                {
                    var moduleRegister = ODApp.Instance.ModuleHub.Get(injectModuleAttribute.ModuleType);
                    if (moduleRegister != null)
                    {
                        var module = moduleRegister.Get(property.PropertyType);
                        if (module != null)
                        {
                            property.SetValue(monoBehaviour, moduleRegister.Get(property.PropertyType));
                        }
                        else
                        {
                            Debug.LogError($"No module of type {property.PropertyType.Name} found when injecting to {monoBehaviour.GetType()}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"No module register with name {injectModuleAttribute.ModuleType} found when injecting to {monoBehaviour.GetType()}");
                    }
                }
            }
        }
    }
}
