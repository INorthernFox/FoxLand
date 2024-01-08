using System.Collections.Generic;
using System.Linq;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using GameFolder.ScriptsFolder.DataFolder.CellDataFolder;
using NaughtyAttributes;
using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder
{
	[CreateAssetMenu(menuName = "Fox/Settings/Cell/MainCellDataCase", fileName = Constants.PrefabsPath.Field.MainCellDataCaseName, order = 0)]
	public class MainCellDataCase : ScriptableObject
	{
		[SerializeField] private CellObject _rootPrefab;
		[SerializeField] private CellDataCase[] _cases;

		public IReadOnlyCollection<CellDataCase> Cases => _cases;
		public CellObject RootPrefab => _rootPrefab;

		[Button()]
		private void ValidateCases()
		{
			CellDataCase[] cellDataCases = Resources.LoadAll<CellDataCase>(Constants.PrefabsPath.Field.PlayingFieldFolder);
			_cases = cellDataCases.OrderBy(p => p.name).ToArray();
		}
	}

}