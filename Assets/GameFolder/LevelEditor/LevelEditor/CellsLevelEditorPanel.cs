using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.Infrastructure.Factory;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class CellsLevelEditorPanel : MonoBehaviour
	{
		[SerializeField] private RectTransform _case;
		[SerializeField] private CellsCaseObject _cellsCaseObjectPrefab;

		private BaseFactory<CellsCaseObject> _factory;
		private Dictionary<string, CellsCaseObject> _cells;

		private CellsCaseObject _currentSelectedCase;

		public event Action<string> NewCellSelected;

		public async UniTask Initialization(CellCacheService cellCacheService)
		{
			string[] ids = cellCacheService.GetAllIds();
			_factory = new BaseFactory<CellsCaseObject>(_case, _cellsCaseObjectPrefab);
			_cells = new Dictionary<string, CellsCaseObject>(ids.Length);

			foreach(string id in ids)
			{
				CellCreateData createData = await cellCacheService.GetCell(id);
				CellsCaseObject cellsCase = _factory.Create();
				cellsCase.Initialization(id, createData.Interactive);
				cellsCase.TriedSelect += CellsCaseOnTriedSelect;
				_cells.Add(id, cellsCase);
			}
		}

		public void Activate() =>
			gameObject.SetActive(true);

		public void Deactivate() =>
			gameObject.SetActive(false);

		public void SelectCase(string id)
		{
			if(!_cells.ContainsKey(id))
				throw new Exception($"[{nameof(CellsLevelEditorPanel)}] There is no container with id {id}");

			if(_currentSelectedCase != null)
				_currentSelectedCase.Deselect();

			_currentSelectedCase = _cells[id];
			_currentSelectedCase.Select();
			NewCellSelected?.Invoke(id);
		}

		private void CellsCaseOnTriedSelect(string id)
		{
			SelectCase(id);
			NewCellSelected?.Invoke(id);
		}

		private void OnDestroy()
		{
			if(_cells == null)
				return;

			foreach(KeyValuePair<string, CellsCaseObject> cellsCase in _cells)
			{
				cellsCase.Value.TriedSelect -= CellsCaseOnTriedSelect;
				Destroy(cellsCase.Value.gameObject);
			}

			_cells.Clear();
		}
	}
}