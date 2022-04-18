using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    [CreateAssetMenu(fileName = "LevelGeneratorSettings", menuName = "Games/Jumper/LevelGeneratorSettings", order = 1)]
    public class LevelGeneratorSettings : ScriptableObject
    {
        public List<GameObject> WallPrefabs;
        public List<GameObject> ObstaclePrefabs;
        public List<GameObject> GroundPrefabs;
        public List<GameObject> PickablePrefabs;
        public GameObject EndTriggerPrefab;
        public float FirstObstacleDistance;
    }
}