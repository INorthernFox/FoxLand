using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder.CellDataFolder
{
	public class CellGameData : ICellGameData
	{
		public CellGameData(string name, string id, Vector2Int position, float rotation, bool interactive)
		{
			Name = name;
			ID = id;
			Position = position;
			Rotation = rotation;
			Interactive = interactive;
		}

		public string Name { get; }
		public string ID { get; }
		public Vector2Int Position { get; }
		public float Rotation { get; }
		public bool Interactive { get; }
	}
}