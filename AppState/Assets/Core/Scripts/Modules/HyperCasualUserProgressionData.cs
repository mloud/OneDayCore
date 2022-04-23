using System.Collections.Generic;
using Core.Modules;

namespace OneDay.Core.Modules
{
    public class HyperCasualProgressionData : AModuleData
    {
        public int Level = 0;
        // level index, stars
        public SortedList<int, int> FinishedLevels = new();
    }
}