using System;

namespace OneDay.Core.GeneralModules
{
    public interface IProgressionModule : IModule
    {
        public Action<(int xpAdded, int currentXp, bool levelChanged, int level)> ProgressionChangedDelegate { get; set; }
        int GetLevel();
        int GetXp();
        int GetXpToNextLevel();
        void AddXp(int xp, string reason = null);
        void ResetXpInLevel();
    }

    public class ProgressionModule : IProgressionModule
    {
        public Action<(int xpAdded, int currentXp, bool levelChanged, int level)> ProgressionChangedDelegate { get; set; }

        private readonly UserProgressionData userProgressionData;
        public ProgressionModule(UserProgressionData userProgressionData)
        {
            this.userProgressionData = userProgressionData;
        }

        public int GetLevel() => userProgressionData.Level;
        public int GetXp() => userProgressionData.Xp;
        public int GetXpToNextLevel() => userProgressionData.Level * 10;
        public void ResetXpInLevel() => userProgressionData.Xp = 0;

        public void AddXp(int xp, string reason = null)
        {
            int startXp = userProgressionData.Xp;
            bool isLevelUp = false;
            while (xp > 0)
            {
                int xpToAdd = Math.Min(xp, GetXpToNextLevel() - userProgressionData.Xp);
                if ((userProgressionData.Xp + xpToAdd) == GetXpToNextLevel())
                {
                    isLevelUp = true;
                    userProgressionData.Level++;
                }
                userProgressionData.Xp += xpToAdd;
                xp -= xpToAdd;
            }
            ProgressionChangedDelegate?.Invoke((xp, userProgressionData.Xp, isLevelUp, userProgressionData.Level));
        }
    }
}
