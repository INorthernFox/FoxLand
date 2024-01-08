using System;
using GameFolder.ScriptsFolder.MenuSceneFolder;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	[Serializable]
	public struct GameSessionConfiguration
	{
		public int LevelNumber;
		public int LevelPlayerNumber;
	}

	[Serializable]
	public struct FieldData
	{
		public CellData[,] CellsData;
	}

	[Serializable]
	public struct CellData
	{
		public string ID;
		public Vector2Int Position;
		public float Rotation;

		public static CellData Default => new CellData();
	}

}