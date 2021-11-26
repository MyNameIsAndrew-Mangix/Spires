using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using Lowscope.Saving.Enums;

namespace Lowscope.Saving.Components
{
    /// <summary>
    /// Used to load the scenes you saved using the SaveLastUsedScenes component.
    /// This is useful to put in a main menu or in an empty scene you load from the main menu.
    /// </summary>
    [AddComponentMenu("Saving/Components/Scene Loading/Load Last Used Scenes"), DisallowMultipleComponent, DefaultExecutionOrder(-9014)]
    public class LoadLastUsedScenes : MonoBehaviour
    {
        [System.Serializable]
        public class Settings
        {
            public LoadTrigger[] loadTriggers = new LoadTrigger[1] { LoadTrigger.OnSlotChanged };
            public bool onlyLoadIfMainSceneIsDifferent = true;
            public bool onlyLoadIfAnyAdditionalScenesAreDifferent = true;
            
            [Tooltip("This will ensure nothing happens if this component is disabled. Useful if you have " +
                     "Load Default slot turned on.")]
            public bool stopListeningIfDisabled;
            
            [Tooltip("Create a new asset by doing create asset Component Save System/Fallback Scene Data")]
            public FallbackSceneData fallbackSceneData;

            [Tooltip("Load this scene if it could not find data for a specific save slot")]
            public string defaultSceneIfEmpty;
        }

        [SerializeField] private Settings settings = new Settings();
        
        private HashSet<LoadTrigger> triggers = new HashSet<LoadTrigger>();

        private bool subscribed;
        
        private void Awake()
        {
            int totalLoadTriggers = settings.loadTriggers.Length;
            for (int i = 0; i < totalLoadTriggers; i++)
            {
                triggers.Add(settings.loadTriggers[i]);
            }

            if (triggers.Contains(LoadTrigger.OnSlotChanged) && !subscribed)
            {
                SaveMaster.OnSlotChangeDone += OnSlotChangeDone;
                subscribed = true;
            }
        }

        private void OnDestroy()
        {
            if (triggers.Contains(LoadTrigger.OnDestroy))
            {
                Load();
            }

            if (triggers.Contains(LoadTrigger.OnSlotChanged) && subscribed)
            {
                SaveMaster.OnSlotChangeDone -= OnSlotChangeDone;
                subscribed = false;
            }
        }

        private void OnDisable()
        {
            if (triggers.Contains(LoadTrigger.OnDisable))
            {
                Load();
            }

            if (settings.stopListeningIfDisabled && subscribed)
            {
                SaveMaster.OnSlotChangeDone -= OnSlotChangeDone;
                subscribed = false;
            }
        }

        private void OnEnable()
        {
            if (triggers.Contains(LoadTrigger.OnEnable))
            {
                Load();
            }
            
            if (settings.stopListeningIfDisabled && !subscribed)
            {
                SaveMaster.OnSlotChangeDone += OnSlotChangeDone;
                subscribed = true;
            }
        }

        private void Start()
        {
            if (triggers.Contains(LoadTrigger.OnStart))
            {
                Load();
            }

            if (triggers.Contains(LoadTrigger.OnSlotChanged))
            {
                var slotChangeInfo = SaveMaster.InitialSlotChangeInfo();

                // Check if first frame
                if (slotChangeInfo.firstFrame && Time.frameCount == 1 && SaveMaster.GetActiveSlot() >= 0)
                {
                    OnSlotChangeDone(slotChangeInfo.toSlot, slotChangeInfo.fromSlot);
                }
            }
        }

        private void OnSlotChangeDone(int newSlot, int fromSlot)
        {
            // Check if slots are valid.
            if (newSlot != -1 && newSlot != -2)
            {
                Load();
            }
        }

        public void Load()
        {
            if (SaveMaster.IsSlotLoaded())
            {
                TimeSpan? timeSpan = SaveMaster.GetTimeSinceLastSave();

                if (timeSpan.HasValue && timeSpan.Value != default(TimeSpan))
                {
                    if (timeSpan.Value.TotalSeconds < 1)
                        return;
                }

                string saveData = SaveMaster.GetString("LastUsedScenes");

                if (string.IsNullOrEmpty(saveData))
                {
                    Debug.Log("Failed to find any scene to load. Is there " +
                              "a SaveLastUsedScenes component in the target scene?");

                    if (!string.IsNullOrEmpty(settings.defaultSceneIfEmpty))
                    {
                        SceneManager.LoadScene(settings.defaultSceneIfEmpty);
                    }

                    return;
                }

                SaveLastUsedScenes.SavedScenes savedScenes = JsonUtility.FromJson<SaveLastUsedScenes.SavedScenes>(saveData);

                if (savedScenes != null)
                {
                    if (settings.fallbackSceneData != null)
                    {
                        string newActiveScene = settings.fallbackSceneData.GetFallbackScene(savedScenes.activeSceneName);
                        if (!string.IsNullOrEmpty(newActiveScene))
                            savedScenes.activeSceneName = newActiveScene;

                        int additionalScenes = savedScenes.additionalSceneNames.Count;
                        for (int i = 0; i < additionalScenes; i++)
                        {
                            string n = savedScenes.additionalSceneNames[i];
                            string newAdditionalScene = settings.fallbackSceneData.GetFallbackScene(n);
                            if (!string.IsNullOrEmpty(newAdditionalScene))
                                savedScenes.additionalSceneNames[i] = newActiveScene;
                        }
                    }
                    
                    if (settings.onlyLoadIfMainSceneIsDifferent)
                    {
                        if (SceneManager.GetActiveScene().name == savedScenes.activeSceneName)
                        {
                            // If the main scene is the same as the active scene, we return.
                            // unless there is still another check we need to do.
                            if (!settings.onlyLoadIfAnyAdditionalScenesAreDifferent)
                                return;
                        }
                    }

                    if (settings.onlyLoadIfAnyAdditionalScenesAreDifferent)
                    {
                        bool differentSceneCount = SceneManager.sceneCount != savedScenes.additionalSceneNames.Count;

                        if (!differentSceneCount)
                        {
                            List<string> sceneNames = new List<string>();
                            int sceneCount = SceneManager.sceneCount;
                            Scene activeScene = SceneManager.GetActiveScene();

                            for (int i = 0; i < sceneCount; i++)
                            {
                                Scene getScene = SceneManager.GetSceneAt(i);
                                if (getScene == activeScene)
                                    continue;

                                sceneNames.Add(getScene.name);
                            }

                            if (savedScenes.additionalSceneNames.SequenceEqual(sceneNames))
                            {
                                // Additive scenes are not different.
                                return;
                            }
                        }
                    }

                    SaveMaster.ClearListeners(false); // Ensure no active listeners get saved

                    SceneManager.LoadScene(savedScenes.activeSceneName);

                    int additiveScenes = savedScenes.additionalSceneNames.Count;
                    for (int i = 0; i < additiveScenes; i++)
                    {
                        SceneManager.LoadScene(savedScenes.additionalSceneNames[i], LoadSceneMode.Additive);
                    }
                }
            }
        }
    }
}