using System.Collections.Generic;
using UnityEngine;

namespace Lowscope.Saving.Components
{
	[System.Serializable]
	public class RerouteSceneSettings
	{
		public string OldSceneName;
		public string NewSceneName;
	}
	
	[CreateAssetMenu(fileName = "Fallback Scene Data", menuName = "Component Save System/Fallback Scene Data")]
	public class FallbackSceneData : ScriptableObject
	{
		public RerouteSceneSettings[] settings;

		private readonly Dictionary<string, string> indexedFallbackScenes = new Dictionary<string, string>();
		
		public string GetFallbackScene(string sceneName)
		{
			if (indexedFallbackScenes.Count == 0)
			{
				if (settings.Length == 0)
				{
					return "";
				}
				
				IndexFallbackScenes();
			}

			string fallbackScene;
			if (indexedFallbackScenes.TryGetValue(sceneName, out fallbackScene))
			{
				return fallbackScene;
			}

			return "";
		}

		private void IndexFallbackScenes()
		{
			int settingCount = settings.Length;
			for (int i = 0; i < settingCount; i++)
			{
				indexedFallbackScenes.Add(settings[i].OldSceneName, settings[i].NewSceneName);
			}
		}
	}
}