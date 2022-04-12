using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core
{
    public class ModuleHub
    {
        private readonly Dictionary<string, ModuleRegister> Registers;

        public ModuleHub()
        {
            Registers = new Dictionary<string, ModuleRegister>();
        }
        public ModuleRegister Get(string name)
        {
            try
            {
                return Registers[name];
            }
            catch(Exception e)
            {
                Debug.LogError($"No such module {name} found");
                throw e;
            }
        }
        public void Register(string name, ModuleRegister register) => Registers.Add(name, register);
    }
}
