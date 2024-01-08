using System;
using System.Collections.Generic;
using System.Linq;
using GameFolder.ScriptsFolder.Core.MapFolder;
using GameFolder.ScriptsFolder.Services.DataServicesFolder;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFolder.ScriptsFolder.DataFolder
{
	[CreateAssetMenu(menuName = "Fox/Settings/Levels/Data", fileName = Constants.LevelSaves.LevelsSaveDataCaseName, order = 0)]
	public class LevelsSaveDataCase : ScriptableObject
	{
		[SerializeField] private LevelsDataDataSubCaseOpen[] _levels;

		public IReadOnlyList<LevelsDataDataSubCaseClose> Levels => GetLevels();

		private IReadOnlyList<LevelsDataDataSubCaseClose> GetLevels()
		{
			List<LevelsDataDataSubCaseClose> cases = new(_levels.Length);
			cases.AddRange(_levels.Select(cell => new LevelsDataDataSubCaseClose(cell)));
			return cases;
		}

#if UNITY_EDITOR
		[Button()]
		private void OnValidate()
		{
			HashSet<string> seenIds = new HashSet<string>();
			
			for( int i = 0; i < _levels.Length; i++ )
			{
				LevelsDataDataSubCaseOpen level = _levels[i];
				
				if(level.CellObject_Editor_Only == null)
					continue;
				
				string fullPath = AssetDatabase.GetAssetPath(level.CellObject_Editor_Only);

				if(!DataSaver.TryLoad(out LevelSaveData levelSaveData, fullPath))
				{
					level.CellObject_Editor_Only = null;
					level.ResourcesPath = null;
					level.ID = $"Object cannot be converted to {nameof(LevelSaveData)}";
					_levels[i] = level;
					continue;
				}
				
				level.ResourcesPath = Constants.LevelSaves.LevelSaveResourcesPath + "\\" + level.ID;
				level.ID = levelSaveData.ID;
				
				if (seenIds.Contains(level.ID))
					level.ID = $"Error {404 + i}";
				
				seenIds.Add(level.ID);
				
				_levels[i] = level;
			}
		}
#endif
	}
	
	
	[Serializable]
	public struct LevelsDataDataSubCaseClose
	{
		public readonly string ID;
		public readonly string Path;
		
		public LevelsDataDataSubCaseClose(LevelsDataDataSubCaseOpen open)
		{
			ID = open.ID;
			Path = open.ResourcesPath;
		}
	}

	[Serializable]
	public struct LevelsDataDataSubCaseOpen
	{
		public string ID;
#if UNITY_EDITOR
		public Object CellObject_Editor_Only;
#endif
		
		public string ResourcesPath;
	}
}