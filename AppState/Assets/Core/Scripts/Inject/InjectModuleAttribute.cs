using System;

namespace OneDay.Core.Inject
{
    public class InjectModuleAttribute : Attribute
    {
        public string ModuleType { get; private set; }

        public InjectModuleAttribute(string moduleType) => ModuleType = moduleType;
    }
}