using System;
using System.Collections.Generic;

namespace OneDay.Core
{
    public class ModuleRegister
    {
        private readonly Dictionary<Type, IModule> modules;

        public ModuleRegister() => modules = new Dictionary<Type, IModule>(); 

        public T Get<T>() where T: IModule
        {
            return (T)modules[typeof(T)];
        }

        public IModule Get(Type type)
        {
            modules.TryGetValue(type, out var module);
            return module;
        }

        public bool Has<T>() where T:IModule
        {
            return modules.ContainsKey(typeof(T));
        }

        public ModuleRegister Register<T>(IModule module)
        {
            modules.Add(typeof(T), module);
            return this;
        }
    }
}
