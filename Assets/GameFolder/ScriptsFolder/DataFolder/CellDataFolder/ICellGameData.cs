using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder.CellDataFolder
{
	public interface ICellGameData
	{
		public string Name { get; }
		public string ID { get; }
		public Vector2Int Position { get; }
		public float Rotation { get; }
		public bool Interactive { get; }
	}


}