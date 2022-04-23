using UnityEngine;

namespace OneDay.Games.Jumper
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Games/Jumper/LevelSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        [Header("Difficulty settings")]
        public float Length;
        public Vector2 ObstacleDistance;
        public float Speed;
        public int Pickables;
        [Header("Visual settings")] public LevelGeneratorSettings levelGeneratorSettings;
        
        [Header("Final level prefab")]
        public Level LevelPrefab;
    }
}