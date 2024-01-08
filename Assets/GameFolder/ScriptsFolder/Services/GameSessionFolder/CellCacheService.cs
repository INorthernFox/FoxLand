using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.DataFolder.CellDataFolder;
using GameFolder.ScriptsFolder.Services.ResourcesFolder;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class CellCacheService : IService, IDisposable
	{
		private readonly IResourcesLoaderService _resourcesLoaderService;

		private readonly Dictionary<string, CellDataSubCaseClose> _cellsCreateInfo = new();
		private readonly Dictionary<string, CellCreateData> _cellsObjects = new();

		private CellObject _rootPrefab;
		
		public CellCacheService(IResourcesLoaderService resourcesLoaderService)
		{
			_resourcesLoaderService = resourcesLoaderService;
			LoadCells();
		}

		public string[] GetAllIds() =>
			_cellsCreateInfo.Keys.ToArray();

		private void LoadCells()
		{
			MainCellDataCase mainCellDataCase = _resourcesLoaderService.Load<MainCellDataCase>(Constants.PrefabsPath.Field.PlayingFieldMainCellDataCasePath);

			_rootPrefab = mainCellDataCase.RootPrefab;
			
			foreach(CellDataCase dataCase in mainCellDataCase.Cases)
			{
				foreach(CellDataSubCaseClose cell in dataCase.Cells)
					_cellsCreateInfo.Add(cell.ID, cell);
			}
		}

		public async UniTask<CellCreateData> GetCell(string id)
		{
			if(string.IsNullOrEmpty(id))
				throw new NullReferenceException($"[{typeof(CellCacheService)}] ID is null");

			if(_cellsObjects.ContainsKey(id))
				return _cellsObjects[id];

			if(!_cellsCreateInfo.ContainsKey(id))
				throw new AggregateException($"[{typeof(CellCacheService)}] ID {id} is not in the system");

			string path = _cellsCreateInfo[id].Path;

			CellSubObject cellSubObject = await _resourcesLoaderService.LoadAsync<CellSubObject>(path);
			CellCreateData createData = new CellCreateData(_rootPrefab, cellSubObject, _cellsCreateInfo[id].Interactive);

			_cellsObjects.Add(id, createData);

			return createData;
		}

		public void Dispose()
		{
			_cellsCreateInfo.Clear();
			_cellsObjects.Clear();
		}
	}

	public struct CellCreateData
	{
		public readonly CellObject Prefab;
		public readonly CellSubObject SubObject;
		public readonly bool Interactive;

		public CellCreateData(CellObject cell, CellSubObject subObject, bool interactive)
		{
			Prefab = cell;
			SubObject = subObject;
			Interactive = interactive;
		}
	}
}