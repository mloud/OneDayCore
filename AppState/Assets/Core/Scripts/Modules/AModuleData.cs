using System;
using Newtonsoft.Json;

namespace Core.Modules
{
    public abstract class AModuleData
    {
        [JsonIgnore]
        public Action Save;
    }
}