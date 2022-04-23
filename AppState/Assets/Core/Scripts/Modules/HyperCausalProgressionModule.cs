using System;
using Core.Modules;
using OneDay.Core.Modules;

namespace OneDay.Core.GeneralModules
{
    public interface IHyperCasualProgressioModule : IModule
    {
        Action<(int level, int stars, bool finished)> LevelUpdated { get; set; }
       
        // returns current level
        int GetLevel();
        
        // returns levels count
        int LevelsCount();
        // returns true if level was finished
        bool IsLevelFinished(int levelIndex);
        // returns number of stars after finishing the level, returns 0 when level was not finished yes
        int GetLevelStars(int levelIndex);
        void FinishLevel(int levelIndex, int stars);
        void UpdateFinishedLevel(int levelIndex, int stars);
    }

    public class HyperCasualProgressionModule : IHyperCasualProgressioModule
    {
        public Action<(int level, int stars, bool finished)> LevelUpdated { get; set; }

        private readonly HyperCasualProgressionData userProgressionData;
        private readonly IHyperCasualWorldData worldProgressionData;
        
        public HyperCasualProgressionModule(HyperCasualProgressionData userProgressionData, IHyperCasualWorldData worldProgressionData)
        {
            this.userProgressionData = userProgressionData;
            this.worldProgressionData = worldProgressionData;
        }

        public int GetLevel() => userProgressionData.Level;

        public int LevelsCount() => worldProgressionData.LevelsCount();

        public bool IsLevelFinished(int levelIndex) => userProgressionData.FinishedLevels.ContainsKey(levelIndex);

        public int GetLevelStars(int levelIndex) => userProgressionData.FinishedLevels.ContainsKey(levelIndex)
            ? userProgressionData.FinishedLevels[levelIndex]
            : 0;

        public void FinishLevel(int levelIndex, int stars)
        {
            //D.Assert(!userProgressionData.FinishedLevels.ContainsKey(levelIndex), $"Level {levelIndex} already finished");
            if (userProgressionData.FinishedLevels.ContainsKey(levelIndex))
            {
                UpdateFinishedLevel(levelIndex, stars);
            }
            else
            {
                userProgressionData.FinishedLevels.Add(levelIndex, stars);
                userProgressionData.Level = levelIndex + 1;
                userProgressionData.Save();
                LevelUpdated?.Invoke((levelIndex, stars, true));
            }
        }

        public void UpdateFinishedLevel(int levelIndex, int stars)
        {
            D.Assert(userProgressionData.FinishedLevels.ContainsKey(levelIndex), $"Level {levelIndex} not finished");
            D.Assert(userProgressionData.FinishedLevels[levelIndex] < stars, $"Cannot update level with less stars {stars}");
            userProgressionData.FinishedLevels[levelIndex] = stars;
            userProgressionData.Save();
            LevelUpdated?.Invoke((levelIndex, stars, false));
        }
    }
}