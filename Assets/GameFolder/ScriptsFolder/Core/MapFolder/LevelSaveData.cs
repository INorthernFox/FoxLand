using System;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;

namespace GameFolder.ScriptsFolder.Core.MapFolder
{
	[Serializable]
	public struct LevelSaveData
	{
		public string ID;
		public FieldData FieldData;
	}
}