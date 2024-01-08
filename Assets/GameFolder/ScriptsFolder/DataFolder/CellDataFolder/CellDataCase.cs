using System;
using System.Collections.Generic;
using System.Linq;
using GameFolder.ScriptsFolder.Core.MapFolder.CellFolder;
using PathCreation;
using UnityEditor;
using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder.CellDataFolder
{

	[CreateAssetMenu(menuName = "Fox/Settings/CellDataCase", fileName = "CellDataCase", order = 0)]
	public class CellDataCase : ScriptableObject
	{
		[SerializeField] private CellDataSubCaseOpen[] _cells;

		public IReadOnlyCollection<CellDataSubCaseClose> Cells => GetCells();

		private IReadOnlyCollection<CellDataSubCaseClose> GetCells()
		{
			List<CellDataSubCaseClose> cases = new(_cells.Length);
			cases.AddRange(_cells.Select(cell => new CellDataSubCaseClose(cell)));
			return cases;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			HashSet<string> seenIds = new HashSet<string>();

			for( int i = 0; i < _cells.Length; i++ )
			{
				CellDataSubCaseOpen cell = _cells[i];

				if(seenIds.Contains(cell.ID))
					cell.ID = $"Error {404 + i}";

				if(cell.CellObject_EditorOnly == null)
					continue;
				
				seenIds.Add(cell.ID);

				string fullPath = AssetDatabase.GetAssetPath(cell.CellObject_EditorOnly);
				cell.Path = fullPath;

				int startIndex = fullPath.IndexOf("Resources/");

				if(startIndex != -1)
				{
					startIndex += "Resources/".Length;
					string trimmedPath = fullPath.Substring(startIndex);
					int extensionIndex = trimmedPath.LastIndexOf(".prefab");

					if(extensionIndex != -1)
						cell.Path = trimmedPath.Remove(extensionIndex);
				}

				_cells[i] = cell;
			}
		}
#endif
	}

	[Serializable]
	public struct CellDataSubCaseClose
	{
		public readonly string ID;
		public readonly string Path;
		public readonly bool Interactive;

		public CellDataSubCaseClose(CellDataSubCaseOpen open)
		{
			ID = open.ID;
			Path = open.Path;
			Interactive = open.Interactive;
		}
	}

	[Serializable]
	public struct CellDataSubCaseOpen
	{
		public string ID;
		public bool Interactive;
#if UNITY_EDITOR
		public CellSubObject CellObject_EditorOnly;
#endif

		public string Path;
	}

	[Serializable]
	public struct CellPaths
	{
		public HexagonSides Start;
		public HexagonSides End;
		public PathCreator Path;
	}
	
	public static class HexagonSidesExtension
	{
		public static Vector3 ToVector(this HexagonSides hexagonSides)
		{
			if(hexagonSides == HexagonSides.Stop)
				return Vector3.zero;

			return Quaternion.Euler(Vector3.up *60 * (int)hexagonSides) *  Vector3.forward;
			
		}

		public static Vector2Int ToStep(this HexagonSides hexagonSides)
		{
			switch(hexagonSides)
			{
				case HexagonSides.Stop:
					return new Vector2Int(0, 0);
				case HexagonSides.Top:
					return new Vector2Int(0, -1);
				case HexagonSides.TopRight:
					return new Vector2Int(1, -1);
				case HexagonSides.BottomRight:
					return new Vector2Int(1, 0);
				case HexagonSides.Bottom:
					return new Vector2Int(0, 1);
				case HexagonSides.BottomLeft:
					return new Vector2Int(-1, 0);
				case HexagonSides.TopLeft:
					return new Vector2Int(-1, -1);
				default:
					throw new ArgumentOutOfRangeException(nameof(hexagonSides), hexagonSides, null);
			}
		}
	}

	public enum HexagonSides
	{
		Stop = -1,
		Top = 0,
		TopRight = 1,
		BottomRight = 2,
		Bottom = 3,
		BottomLeft = 4,
		TopLeft = 5,
	}
}