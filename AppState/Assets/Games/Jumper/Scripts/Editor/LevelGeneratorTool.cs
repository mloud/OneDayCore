#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace OneDay.Games.Jumper.Editor
{
    public static class LevelGeneratorTool
    {
        static float StartOffset = 10;
        static float EndOffset = 10;
        static float WallDistance = 2;
        static Vector2 pickableHeight = new Vector2(1.5f, 2.5f);
        
        public static void Generate(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings, GameObject existingLevel)
        {
            GameObject levelRoot = null;
            if (existingLevel == null)
            {
                levelRoot = new GameObject("Level");
            }
            else
            {
                levelRoot = existingLevel;
                ClearLevel(levelRoot);
            }

            CreateLevelHierarchy(levelRoot, levelSettings);

            CreateWalls(generatorSettings, levelSettings, levelRoot.transform.Find("Walls").gameObject);
            CreateGround(generatorSettings, levelSettings, levelRoot.transform.Find("Ground").gameObject);
            CreateObstacles(generatorSettings, levelSettings, levelRoot.transform.Find("Obstacles").gameObject);
            CreatePickables(generatorSettings, levelSettings, levelRoot.transform.Find("Pickables").gameObject);
            CreateTriggers(generatorSettings, levelSettings, levelRoot.transform.Find("Triggers").gameObject);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
#endif
        }

        private static void CreateLevelHierarchy(GameObject go, LevelSettings levelSettings)
        {
            var groundGo = new GameObject("Ground");
            groundGo.transform.SetParent(go.transform);

            var wallsGo = new GameObject("Walls");
            wallsGo.transform.SetParent(go.transform);

            var objectsGo = new GameObject("Obstacles");
            objectsGo.transform.SetParent(go.transform);

            var pickablesGo = new GameObject("Pickables");
            pickablesGo.transform.SetParent(go.transform);

            var triggersGo = new GameObject("Triggers");
            triggersGo.transform.SetParent(go.transform);

            go.AddComponent<Level>().LevelSettings = levelSettings;
        }

        private static void ClearLevel(GameObject go)
        {
            while (go.transform.childCount > 0)
            {
                GameObject.DestroyImmediate(go.transform.GetChild(0).gameObject);
            }

            var level = go.GetComponent<Level>();
            if (level != null)
            {
                GameObject.DestroyImmediate(level);
            }
        }

        private static void CreateWalls(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings, GameObject wallContainer)
        {
            float x = -StartOffset;
            while (x < levelSettings.Length + EndOffset)
            {
                var randWall = generatorSettings.WallPrefabs.GetRandomElement();
             
                // bottom wall
                var wall = (GameObject)PrefabUtility.InstantiatePrefab(randWall);
                wall.transform.SetParent(wallContainer.transform);
                wall.transform.SetLocalPositionX(x);
                wall.transform.SetLocalPositionY(0);
                wall.transform.SetLocalPositionZ(WallDistance);

                // top wall
                wall = (GameObject)PrefabUtility.InstantiatePrefab(randWall);
                wall.transform.SetParent(wallContainer.transform);
                wall.transform.SetLocalPositionX(x);
                wall.transform.SetLocalPositionY(wall.GetComponent<Collider>().bounds.size.y);
                wall.transform.SetLocalPositionZ(WallDistance);

                x += wall.GetComponent<Collider>().bounds.size.x;
            }
        }
        
        private static void CreateGround(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings, GameObject groundContainer)
        {
            float x = -StartOffset;
            while (x < levelSettings.Length + EndOffset)
            {
                var randGround = generatorSettings.GroundPrefabs.GetRandomElement();
                // bottom wall
                var ground = (GameObject)PrefabUtility.InstantiatePrefab(randGround);
                var bounds = ground.GetComponent<Collider>().bounds; 
            
                ground.transform.SetParent(groundContainer.transform);
                ground.transform.SetLocalPositionX(x);
                ground.transform.SetLocalPositionY(0);
                ground.transform.SetLocalPositionZ(0);

                x += bounds.size.x;
            }
        }

        private static void CreateObstacles(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings,
            GameObject obstacleContainer)
        {
            float x = generatorSettings.FirstObstacleDistance;

            while (x < levelSettings.Length)
            {
                var rndObstacle = generatorSettings.ObstaclePrefabs.GetRandomElement();
                var obstacle = (GameObject)PrefabUtility.InstantiatePrefab(rndObstacle);
                var bounds = obstacle.GetComponent<Collider>().bounds; 
       
                obstacle.transform.SetParent(obstacleContainer.transform);
                obstacle.transform.SetLocalPositionX(x);
                obstacle.transform.SetLocalPositionX(x);
                obstacle.transform.SetLocalPositionZ(0);  
                x += bounds.size.x / 2 +
                     Random.Range(levelSettings.ObstacleDistance.x, levelSettings.ObstacleDistance.y);
            }
        }
        
        private static void CreatePickables(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings,
            GameObject pickableContainer)
        {
            float x = generatorSettings.FirstObstacleDistance;
            float distanceBetweenPickables = (float)levelSettings.Length / levelSettings.Pickables;
            
            while (x < levelSettings.Length)
            {
                var rndPickable = generatorSettings.PickablePrefabs.GetRandomElement();
                var pickable = (GameObject)PrefabUtility.InstantiatePrefab(rndPickable);
                pickable.transform.SetParent(pickableContainer.transform);
                pickable.transform.SetLocalPositionX(x);
                pickable.transform.SetLocalPositionY(Random.Range(pickableHeight.x, pickableHeight.y));
                pickable.transform.SetLocalPositionZ(0);  
                x += Random.Range(distanceBetweenPickables * 0.8f, distanceBetweenPickables * 1.2f);
            }
        }

        private static void CreateTriggers(LevelGeneratorSettings generatorSettings, LevelSettings levelSettings,
            GameObject triggerContainer)
        {
            var endTrigger = (GameObject)PrefabUtility.InstantiatePrefab(generatorSettings.EndTriggerPrefab);
            endTrigger.transform.SetParent(triggerContainer.transform);
            endTrigger.transform.SetLocalPositionX(levelSettings.Length);
            endTrigger.transform.SetLocalPositionZ(0);
        }
    }
}