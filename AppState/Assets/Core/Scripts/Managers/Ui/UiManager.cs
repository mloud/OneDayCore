using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Core.Ui
{
    public class UiManager : ABaseManager
    {
        [SerializeField] Canvas uiCanvas;
        [SerializeField] SceneUiContainer defaultUiContainer;
        [SerializeField] SceneUiImporter sceneUiImporter;


        private Dictionary<string, List<Transform>> importedSceneUi;

        protected override void Awake()
        {
            base.Awake();
            importedSceneUi = new Dictionary<string, List<Transform>>();
        }

        protected override void InternalInitialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        protected override void InternalRelease()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public IEnumerator Show(string id, KeyValueData data, Action onHide = null)
        {
            var dialog = defaultUiContainer.DialogContainer.Find(id);
            if (dialog != null)
                yield return StartCoroutine(dialog.GetComponent<IShowable>().Show(data, onHide));

            var panel = defaultUiContainer.PanelContainer.Find(id);
            if (panel != null)
                yield return StartCoroutine(panel.GetComponent<IShowable>().Show(data, onHide));
        }

        public IEnumerator Hide(string id)
        {
            var dialog = defaultUiContainer.DialogContainer.Find(id);
            if (dialog != null)
                yield return StartCoroutine(dialog.GetComponent<IShowable>().Hide());

            var panel = defaultUiContainer.PanelContainer.Find(id);
            if (panel != null)
                yield return StartCoroutine(panel.GetComponent<IShowable>().Hide());
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg)
        {
            ImportSceneUi(scene);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            DeleteSceneUi(scene);
        }

        private void ImportSceneUi(Scene scene)
        {
            foreach (var go in scene.GetRootGameObjects())
            {
                var sceneUiContainer = go.GetComponent<SceneUiContainer>();
                if (sceneUiContainer != null)
                {
                    Debug.Log($"Importing canvas {sceneUiContainer.name} from scene {scene.name}");
                    
                    var importedUi = sceneUiImporter.Import(sceneUiContainer, defaultUiContainer);
                    importedSceneUi.Add(scene.name, importedUi);

                    sceneUiContainer.gameObject.SetActive(false);
                }
            }
        }

        private void DeleteSceneUi(Scene scene)
        {
            if (importedSceneUi.TryGetValue(scene.name, out var sceneTransform))
            {
                D.Info($"Scene {scene.name} DOES contain ui that will be deleted");
                foreach(var tr in sceneTransform)
                {
                    Destroy(tr.gameObject);
                }
                importedSceneUi.Remove(scene.name);
            }
            else
            {
                D.Info($"Scene {scene.name} does not contain any ui to delete");
            }
        }
    }
}