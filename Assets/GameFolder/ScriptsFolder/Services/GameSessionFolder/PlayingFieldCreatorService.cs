using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFolder.ScriptsFolder.Core.MapFolder;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.DataFolder.CellDataFolder;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public class PlayingFieldCreatorService : IService
	{
		private readonly CellCacheService _cellCacheService;
		private readonly FieldSettings _fieldSettings;

		public PlayingFieldCreatorService(CellCacheService cellCacheService, GameConstantsSettingsValue gameConstantsSettingsValue)
		{
			_cellCacheService = cellCacheService;
			_fieldSettings = gameConstantsSettingsValue.FieldSettings;
		}

		public async UniTask<PlayingField> Create(FieldData fieldData, Transform rootObject)
		{
			PlayingField playingField = await CreatePlayingField(rootObject, fieldData);
			return playingField;
		}

		private async Task<CellsInfoCase> CreateCells(FieldData fieldData, Transform rootObject)
		{
			CellData[,] cellsData = fieldData.CellsData;
			Vector2 cellSize = _fieldSettings.CellSize + _fieldSettings.Gap;
			Material projectionMaterial = _fieldSettings.ProjectionMaterial;

			float displacementCoefficient = _fieldSettings.HexagonDisplacementCoefficient;

			int xSize = cellsData.GetLength(0);
			int zSize = cellsData.GetLength(1);

			CellObject[,] cellsArray = new CellObject[xSize, zSize];
			Dictionary<CellObject, CellGameData> cellsGameData = new(xSize * zSize);

			for( int z = 0; z < zSize; z++ )
			{
				for( int x = 0; x < xSize; x++ )
				{
					CellData cellData = cellsData[x, z];
					CellObject cellObject = null;

					if(!cellData.Equals(CellData.Default))
						cellObject = await CreateCell(rootObject, cellData, cellSize, displacementCoefficient, projectionMaterial, cellsGameData);

					cellsArray[cellData.Position.x, cellData.Position.y] = cellObject;
				}
			}

			return new CellsInfoCase(cellsArray, cellsGameData);
		}

		private async Task<CellObject> CreateCell(Transform rootObject, CellData cellData, Vector2 cellSize, float displacementCoefficient, Material projectionMaterial, Dictionary<CellObject, CellGameData> cellsGameData)
		{
			CellCreateData createData = await _cellCacheService.GetCell(cellData.ID);

			CellObject cellObject = CreateMainCellObject(rootObject, createData, cellSize, displacementCoefficient, cellData);
			CellSubObject cellSubObject = Object.Instantiate(createData.SubObject, cellObject.transform);
			ProjectionCellObject projection = TryCreateProjectionCellObject(createData, cellObject, cellSize, displacementCoefficient, cellData, projectionMaterial);

			CellGameData cellGameData = new CellGameData(cellObject.name, cellData.ID, cellData.Position.ToUnity(), cellData.Rotation, createData.Interactive);
			cellsGameData.Add(cellObject, cellGameData);
			cellObject.Initialization(cellGameData, cellSubObject, projection);
			return cellObject;
		}

		private async UniTask<PlayingField> CreatePlayingField(Transform rootObject, FieldData fieldData)
		{
			Transform fieldRoot = new GameObject($"{nameof(PlayingField)}").transform;
			fieldRoot.SetParent(rootObject);
			fieldRoot.localPosition = Vector3.zero;
			fieldRoot.localEulerAngles = Vector3.zero;
			fieldRoot.localScale = Vector3.one;
			PlayingField playingField = fieldRoot.AddComponent<PlayingField>();

			CellsInfoCase cellsInfoCase = await CreateCells(fieldData, fieldRoot.transform);

			float playingFieldXOffset = cellsInfoCase.Cells[cellsInfoCase.Cells.GetLength(0) - 1, cellsInfoCase.Cells.GetLength(1) - 1].transform.localPosition.x / -2;
			playingField.transform.localPosition = new Vector3(playingFieldXOffset, 0, 0);
			playingField.SetCells(cellsInfoCase.Cells, cellsInfoCase.CellsGameData, fieldRoot);
			return playingField;
		}

		private static CellObject CreateMainCellObject(Transform rootObject, CellCreateData createData, Vector2 cellSize, float displacementCoefficient, CellData cellData)
		{
			CellObject cellObject = Object.Instantiate(createData.Prefab, rootObject);
			SetMainTransformValue(cellObject, cellSize, displacementCoefficient, cellData);
			SetMainName(cellData, cellObject);
			return cellObject;
		}

		private static ProjectionCellObject TryCreateProjectionCellObject(CellCreateData createData, CellObject cellObject, Vector2 cellSize, float displacementCoefficient, CellData cellData, Material projectionMaterial)
		{
			if(!createData.Interactive)
				return null;

			CellSubObject cellSubObject = Object.Instantiate(createData.SubObject, cellObject.transform);

			Transform cellTransform = cellSubObject.transform;

			cellTransform.localPosition = Vector3.zero;
			cellTransform.localEulerAngles = Vector3.zero;
			cellTransform.localScale = Vector3.one;

			SetProjectionName(cellData, cellSubObject);
			cellSubObject.CellVisualModel.RecolorAllObject(projectionMaterial);

			ProjectionCellObject projection = cellSubObject.AddComponent<ProjectionCellObject>();
			projection.gameObject.SetActive(false);

			return projection;
		}

		private static void SetMainTransformValue(CellObject cellObject, Vector2 cellSize, float displacementCoefficient, CellData cellData)
		{
			Transform cellTransform = cellObject.transform;

			bool even = cellData.Position.x % 2 == 0;
			float offsetZ = even ? 0 : cellSize.y / 2;

			float targetX = cellSize.x * displacementCoefficient * cellData.Position.x;
			float targetY = 0;
			float targetZ = cellSize.y * cellData.Position.y + offsetZ;

			cellTransform.localPosition = new Vector3(targetX, targetY, targetZ);
			cellTransform.localEulerAngles = new Vector3(0, cellData.Rotation, 0);
			cellTransform.localScale = Vector3.one;
		}

		private static void SetMainName(CellData cellData, CellObject cellObject) =>
			cellObject.name = $"[X|{cellData.Position.x + 1} Y|{cellData.Position.y + 1}]_[{cellData.ID}]";

		private static void SetProjectionName(CellData cellData, MonoBehaviour projectionCell) =>
			projectionCell.name = $"[Projection]_[X|{cellData.Position.x + 1} Y|{cellData.Position.y + 1}]_[{cellData.ID}]";

		public struct CellsInfoCase
		{
			public readonly CellObject[,] Cells;
			public readonly Dictionary<CellObject, CellGameData> CellsGameData;

			public CellsInfoCase(CellObject[,] cells, Dictionary<CellObject, CellGameData> data)
			{
				Cells = cells;
				CellsGameData = data;
			}
		}
	}
}