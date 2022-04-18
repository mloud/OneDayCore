using UnityEngine;
using UnityEditor;

namespace OneDay.Games.Jumper.Editor
{
    public class LevelGeneratorWindow : EditorWindow
    {
        private LevelGeneratorSettings generatorSettings;
        private LevelSettings levelSettings;
        private GameObject levelRoot;
        
        [MenuItem("Games/Jumper/Level Generator")]
        static void Init()
        {
            var window = (LevelGeneratorWindow) GetWindow(typeof(LevelGeneratorWindow));
            window.titleContent = new GUIContent("Level generator");
            window.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            generatorSettings = (LevelGeneratorSettings)EditorGUILayout.ObjectField("Generator settings", generatorSettings, typeof(LevelGeneratorSettings), false);
            levelSettings = (LevelSettings)EditorGUILayout.ObjectField("Level settings", levelSettings, typeof(LevelSettings), false);
            levelRoot = (GameObject)EditorGUILayout.ObjectField("Level root", levelRoot, typeof(GameObject), true);

            EditorGUILayout.Space();

            GUI.enabled = generatorSettings != null && levelSettings != null;
            bool generateClicked = GUILayout.Button("Generate");
            GUI.enabled = true;
            if (generateClicked)
            {
                LevelGeneratorTool.Generate(generatorSettings, levelSettings, levelRoot);
            }
        }
    }
}