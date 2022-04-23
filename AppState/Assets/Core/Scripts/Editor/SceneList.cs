using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


namespace Core.Scripts.Editor
{
    public class SceneList : EditorWindow
    {
        [MenuItem("OneDay/Scene List")]
        static void Init()
        {
            var window = (SceneList)EditorWindow.GetWindow(typeof(SceneList));
            window.Show();
            window.titleContent = new GUIContent("Scene List");
        }

        void OnGUI()
        {
            foreach(var scene in EditorBuildSettings.scenes)
            {
                var sceneName = new FileInfo(scene.path).Name.Replace(".unity", "");
        
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(sceneName, GUILayout.Width(100));
                if (GUILayout.Button("Play"))
                {
                    EditorSceneManager.OpenScene(scene.path);
                    EditorApplication.isPlaying = true;
                }

                if (GUILayout.Button("Open"))
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}