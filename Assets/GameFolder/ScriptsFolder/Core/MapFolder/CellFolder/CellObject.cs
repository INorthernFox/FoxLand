using System;
using GameFolder.ScriptsFolder.DataFolder.CellDataFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class CellObject : MonoBehaviour
	{
		[SerializeField] private CellPhysicalModel _cellPhysicalModel;

		private CellSubObject _cellSubObject;
		private CellGameData _cellGameData;
		private ProjectionCellObject _projection;

		public ICellGameData CellData => _cellGameData;
		public CellVisualModel CellSubObject => _cellSubObject.CellVisualModel;
		public CellPhysicalModel CellPhysicalModel => _cellPhysicalModel;

		public void Initialization(CellGameData cellGameData,CellSubObject cellSubObject, ProjectionCellObject projection = null)
		{
			if(_cellGameData != null)
				throw new Exception($"[{typeof(CellObject)}] Reinitializing an CellObject named {cellGameData.Name}");

			if(cellGameData == null)
				throw new Exception($"[{typeof(CellObject)}] CellGameData is null");
			
			if(cellSubObject == null)
				throw new ArgumentNullException($"[{typeof(CellObject)}] CellVisualModel is null an CellObject named {cellGameData.Name}");

			if(cellGameData.Interactive && projection == null)
				throw new Exception($"[{typeof(CellObject)}] CellObject {cellGameData.Name} has no projection");

			_projection = projection;
			_cellGameData = cellGameData;
			_cellSubObject = cellSubObject;
		}

		public bool TryGetProjection(out ProjectionCellObject projection)
		{
			projection = _projection;
			return _cellGameData.Interactive && projection != null;
		}
	}

}