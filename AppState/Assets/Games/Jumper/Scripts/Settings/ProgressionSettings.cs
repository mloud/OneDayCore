using System.Collections.Generic;
using Core.Modules;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    [CreateAssetMenu(fileName = "ProgressionSettings", menuName = "Games/Jumper/ProgressionSettings", order = 1)]
    public class ProgressionSettings : ScriptableObject, IHyperCasualWorldData
    {
        public List<LevelSettings> Levels;
        public int LevelsCount() => Levels.Count;
    }
}