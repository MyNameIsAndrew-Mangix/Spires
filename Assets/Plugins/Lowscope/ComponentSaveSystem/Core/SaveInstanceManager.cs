using UnityEngine;
using System.Collections.Generic;
using Lowscope.Saving.Components;
using Lowscope.Saving.Enums;
using Lowscope.Saving.Data;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;
using System.Security.AccessControl;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Lowscope.Saving.Core
{
	/// <summary>
	/// Each scene has a Save Instance Manager
	/// The responsibility for this manager is to keep track of all saved instances within that scene.
	/// Examples of saved instances are keys or items you have dropped out of your inventory.
	/// </summary>
	[DefaultExecutionOrder(-9100), AddComponentMenu("")]
	public class SaveInstanceManager : MonoBehaviour
	{
		private Dictionary<SavedInstance, SpawnInfo> spawnInfo = new Dictionary<SavedInstance, SpawnInfo>();
		private HashSet<string> loadedIDs = new HashSet<string>();
		private Dictionary<string, CachedPrefab> cachedResourcePrefabs = new Dictionary<string, CachedPrefab>();

		private int spawnCountHistory;
		private int changesMade;

		public string SceneID { set; get; }
		public Scene Scene { set; get; }
		public Saveable Saveable { set; get; }

		public int LoadedIDCount
		{
			get { return loadedIDs.Count; }
		}

		public string SaveID { get; internal set; }

		private List<SpawnInfo> throttledSpawnList = new List<SpawnInfo>();

		private bool spawnThrottled;

		private SaveData saveData = new SaveData()
		{
			infoCollection = new List<SpawnInfo>(),
			spawnCountHistory = 0
		};

		private bool quittingApplication;

		public enum SpawnSource
		{
			FromSave,
			FromUserCode
		}

		public class CachedPrefab
		{
			public GameObject prefab;
			public string[] componentIDs;
			public bool valid;
		}

		[System.Serializable]
		public class SaveData
		{
			public List<SpawnInfo> infoCollection;
			public int spawnCountHistory;
		}

		[System.Serializable]
		public struct SpawnInfo
		{
			public InstanceSource source;
			public string filePath;
			public string saveIdentification;
			public string customSource;
			public string userTag;

			[System.NonSerialized] public SpawnSource spawnSource;

			public bool IsValidData()
			{
				bool filePathEmpty = string.IsNullOrEmpty(filePath);
				bool saveIdEmpty = string.IsNullOrEmpty(saveIdentification);
				bool invalidCustomSource = source == InstanceSource.Custom && string.IsNullOrEmpty(customSource);

				return !filePathEmpty && !saveIdEmpty && !invalidCustomSource;
			}
		}

		public void DestroyAllObjects()
		{
			List<SavedInstance> instances = new List<SavedInstance>();

			foreach (var item in spawnInfo)
			{
				if (item.Key != null)
				{
					instances.Add(item.Key);
				}
			}

			int totalInstanceCount = instances.Count;
			for (int i = 0; i < totalInstanceCount; i++)
			{
				instances[i].Destroy();
			}

			spawnInfo.Clear();
			loadedIDs.Clear();
			spawnCountHistory = 0;
			changesMade++;
		}

		public void ClearData()
		{
			spawnInfo.Clear();
			loadedIDs.Clear();
			spawnCountHistory = 0;
			changesMade++;
		}

		public void DestroyObject(SavedInstance savedInstance, Saveable saveable)
		{
			if (spawnInfo.ContainsKey(savedInstance))
			{
				spawnInfo.Remove(savedInstance);
				loadedIDs.Remove(saveable.SaveIdentification);

				changesMade++;
			}
		}

		public SavedInstance SpawnObject(InstanceSource source, string filePath, string customSourcePath = "",
			string saveIdentification = "", string userTag = "", SpawnSource spawnSource = SpawnSource.FromUserCode,
			bool spawnInactive = false)
		{
			var prefabData = GetPrefabData(source, filePath, customSourcePath);
			if (!prefabData.valid)
			{
				return null;
			}

			changesMade++;

			var prefab = prefabData.prefab;

			// We will temporarily set the resource to disabled. Because we don't want to enable any
			// of the components yet.
			bool resourceState = prefab.activeSelf;
			prefab.SetActive(false);

			GameObject instance = GameObject.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
			SceneManager.MoveGameObjectToScene(instance.gameObject, Scene);

			// After instantiating we reset the resource back to it's original state.
			prefab.SetActive(resourceState);

			Saveable saveable = instance.GetComponent<Saveable>();

			if (saveable == null)
			{
				Debug.LogWarning("Save Instance Manager: No saveable added to spawned object." +
				                 " Scanning for ISaveables during runtime is more costly.");
				saveable = instance.AddComponent<Saveable>();
				saveable.ScanAddSaveableComponents();
			}

			SavedInstance savedInstance = instance.AddComponent<SavedInstance>();

			SpawnInfo info = new SpawnInfo()
			{
				filePath = filePath,
				saveIdentification = saveable.SaveIdentification,
				source = source,
				customSource = customSourcePath,
				userTag = userTag,
				spawnSource = spawnSource
			};

			// In case the object has no idenfication, which applies to all prefabs.
			// Then we give it a new identification, and we store it into our spawninfo array so we know to spawn it again.
			if (string.IsNullOrEmpty(saveIdentification))
			{
				saveable.SaveIdentification = string.Format("{0}-{1}-{2}", SceneID, saveable.name, spawnCountHistory);

				info.saveIdentification = saveable.SaveIdentification;

				spawnInfo.Add(savedInstance, info);

				spawnCountHistory++;

				loadedIDs.Add(saveable.SaveIdentification);
			}
			else
			{
				saveable.SaveIdentification = saveIdentification;
				loadedIDs.Add(saveable.SaveIdentification);
			}

			savedInstance.Configure(saveable, this, info);

			if (!spawnInactive)
			{
				instance.gameObject.SetActive(true);
			}

			if (SaveMaster.OnSpawnedSavedInstance != null)
			{
				SaveMaster.OnSpawnedSavedInstance.Invoke(Scene, savedInstance);
			}

			return savedInstance;
		}

		public void OnSave(SaveGame saveGame)
		{
			SaveSettings settings = SaveSettings.Get();

			if (changesMade > 0)
			{
				changesMade = 0;
				int spawnedInstances = spawnInfo.Count;

				if (spawnedInstances == 0)
				{
					saveGame.Remove(SaveID);
					return;
				}

				saveData.infoCollection.Clear();
				saveData.spawnCountHistory = spawnCountHistory;

				SaveData data = new SaveData()
				{
					infoCollection = new List<SpawnInfo>(),
					spawnCountHistory = this.spawnCountHistory
				};

				foreach (var item in spawnInfo)
				{
					SavedInstance instance = item.Key;
					Saveable saveable = instance.Saveable;

					if (instance.DontSaveInstance)
					{
						SaveMaster.WipeSaveable(saveable);
						continue;
					}

					if (settings.cleanEmptySavedPrefabs)
					{
						// Dont save saveable if no saves and loads have been done.
						if (!saveable.HasLoadedAnyComponents && !saveable.HasSavedAnyComponents)
						{
							continue;
						}
					}

					data.infoCollection.Add(item.Value);
				}

				string json = JsonUtility.ToJson(data);
				saveGame.Set(SaveID, json, SceneID);
			}
		}

		public void OnLoad(SaveGame saveGame)
		{
			SaveSettings settings = SaveSettings.Get();
			SaveData saveData = saveGame != null ? JsonUtility.FromJson<SaveData>(saveGame.Get(SaveID)) : null;

			spawnThrottled = CanSpawnThrottled();

			if (saveData != null && saveData.infoCollection != null)
			{
				spawnCountHistory = saveData.spawnCountHistory;

				int itemCount = saveData.infoCollection.Count;

				for (int i = 0; i < itemCount; i++)
				{
					SpawnInfo savedSpawnInfo = saveData.infoCollection[i];

					// Skip loading this saved instance if data is invalid.
					if (!saveData.infoCollection[i].IsValidData())
					{
						changesMade++;
						continue;
					}

					if (loadedIDs.Contains(savedSpawnInfo.saveIdentification))
					{
						return;
					}

					var source = savedSpawnInfo.source;
					var filePath = savedSpawnInfo.filePath;
					var id = savedSpawnInfo.saveIdentification;
					var sourceId = savedSpawnInfo.customSource;

					var prefabData = GetPrefabData(source, filePath, sourceId);
					if (!prefabData.valid)
					{
						Debug.LogFormat("Unable to spawn saveable, " +
						                "because source ({1}) or path ({0}) has become invalid.", filePath, sourceId);
						changesMade++;
						continue;
					}

					if (settings.cleanEmptySavedPrefabs)
					{
						bool hasExistingData = false;
						int componentCount = prefabData.componentIDs.Length;
						for (int i2 = 0; i2 < componentCount; i2++)
						{
							string dataString = saveGame.Get(string.Format("{0}-{1}", id, prefabData.componentIDs[i2]));

							if (!string.IsNullOrEmpty(dataString))
							{
								hasExistingData = true;
								break;
							}
						}

						// Dont spawn if there is nothing saved based on the spawned prefab.
						if (!hasExistingData)
						{
							changesMade++;
							continue;
						}
					}

					if (spawnThrottled)
					{
						throttledSpawnList.Add(new SpawnInfo()
						{
							source = source,
							filePath = filePath,
							customSource = sourceId,
							saveIdentification = id,
							userTag = savedSpawnInfo.userTag,
							spawnSource = SpawnSource.FromSave
						});
					}
					else
					{
						var obj = SpawnObject(source, filePath, sourceId, id, savedSpawnInfo.userTag,
							SpawnSource.FromSave);
						spawnInfo.Add(obj, savedSpawnInfo);
					}
				}

				// Compatibility for projects that did not save spawnCountHistory
				// Does not get executed for newer projects
				if (spawnCountHistory == 0 && itemCount != 0)
				{
					foreach (var item in spawnInfo.Values)
					{
						string id = item.saveIdentification;
						int getSpawnID = int.Parse(id.Substring(id.LastIndexOf('-') + 1));

						if (getSpawnID > spawnCountHistory)
						{
							spawnCountHistory = getSpawnID + 1;
						}
					}
				}

				if (spawnThrottled)
				{
					StartCoroutine(SpawnPrefabsThrottled());
				}
			}
		}

		public bool CanSpawnThrottled()
		{
			var settings = SaveSettings.Get();

			if (!settings.throttleOptions.spawnThrottled)
				return false;

			if (!settings.throttleOptions.onlyThrottleSpecificScenes)
				return true;
			
			int c = settings.throttleOptions.throttleForScenes.Length;
			string sceneName = Scene.name;

			for (int i = 0; i < c; i++)
			{
				if (settings.throttleOptions.throttleForScenes[i] == sceneName)
				{
					return true;
				}
			}
			
			return false;
		}

		IEnumerator SpawnPrefabsThrottled()
		{
			var settings = SaveSettings.Get();
			int targetSpawnFramerate = settings.throttleOptions.throttleTargetFramerate;
			int taretSpawnMilliseconds = settings.throttleOptions.throttleTargetMilliseconds;
			bool keepDisabledUntilDone = settings.throttleOptions.keepPrefabsDisabledUntilDone;

			int spawnTotal = throttledSpawnList.Count;
			int spawnCount = 0;

			SaveMaster.OnStartSpawningInstances(Scene, spawnTotal);

			Stopwatch sw = new Stopwatch();

			List<GameObject> disabledObjects = new List<GameObject>();

			for (int i = spawnTotal - 1; i >= 0; i--)
			{
				var s = throttledSpawnList[i];

				if (!sw.IsRunning)
				{
					sw.Start();
				}

				var savedInstance = SpawnObject(s.source, s.filePath, s.customSource, s.saveIdentification, s.userTag,
					s.spawnSource, keepDisabledUntilDone);
				spawnInfo.Add(savedInstance, s);

				if (keepDisabledUntilDone)
				{
					disabledObjects.Add(savedInstance.gameObject);
				}

				throttledSpawnList.RemoveAt(i);
				spawnCount++;

				SaveMaster.OnSpawnInstanceProgress(Scene, spawnCount, spawnTotal, (float) spawnCount / spawnTotal);

				bool underTargetFramerate = (int) (1.0f / Time.smoothDeltaTime) < targetSpawnFramerate;
				bool aboveTargetMilliseconds = sw.ElapsedMilliseconds >= taretSpawnMilliseconds;

				if (underTargetFramerate || aboveTargetMilliseconds)
				{
					yield return null;
					sw.Reset();
					sw.Stop();
				}
			}

			int disabledObjectCount = disabledObjects.Count;
			for (int i = 0; i < disabledObjectCount; i++)
			{
				disabledObjects[i].gameObject.SetActive(true);
			}

			SaveMaster.OnFinishSpawningInstances(Scene);
		}

		public CachedPrefab GetPrefabData(InstanceSource source, string path, string customSourcePath = "")
		{
			if (string.IsNullOrEmpty(path))
			{
				return new CachedPrefab()
				{
					valid = false,
				};
			}

			string resourceIdentifier = string.Format("{0}{1}", source.ToString(), path.ToString());
			CachedPrefab cachedPrefab;

			if (cachedResourcePrefabs.TryGetValue(resourceIdentifier, out cachedPrefab))
			{
				return cachedPrefab;
			}
			else
			{
				cachedPrefab = new CachedPrefab();

				switch (source)
				{
					case InstanceSource.Resources:
						cachedPrefab.prefab = Resources.Load(path) as GameObject;
						break;
					case InstanceSource.Custom:
						cachedPrefab.prefab = SaveMaster.GetPrefabResource(customSourcePath, path);
						break;
					default:
						break;
				}

				// Check if resource is valid
				if (cachedPrefab.prefab == null)
				{
					Debug.LogWarning(string.Format("Invalid resource({0}) path: {1}", source.ToString(), path));
					cachedPrefab.valid = false;
					return cachedPrefab;
				}

				// Get saveable
				Saveable saveable = cachedPrefab.prefab.GetComponent<Saveable>();
				if (saveable == null)
				{
					Debug.LogError(string.Format("Prefab at path({0}) " +
					                             "has no saveable component: {1}", source.ToString(), path));
					cachedPrefab.valid = false;
					return cachedPrefab;
				}

				// Get all component id's required for checking if a specific instance
				// Still has any savedata assigned to it.
				var cachedSaveableComponents = saveable.CachedSaveableComponents;
				int componentCount = cachedSaveableComponents.Count;
				cachedPrefab.componentIDs = new string[componentCount];
				for (int i = 0; i < componentCount; i++)
				{
					cachedPrefab.componentIDs[i] = cachedSaveableComponents[i].identification.Value;
				}

				cachedPrefab.valid = true;

				// Cache all found data.
				cachedResourcePrefabs.Add(resourceIdentifier, cachedPrefab);

				return cachedPrefab;
			}
		}
	}
}