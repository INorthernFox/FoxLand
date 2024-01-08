using System;
using GameFolder.ScriptsFolder.DataFolder.CellDataFolder;
using PathCreation;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Core.MapFolder.CellFolder
{
	public class CellPathSettings : MonoBehaviour
	{
		[SerializeField] private CellPaths[] _paths;

		public PathStepData[] GetPaths()
		{
			PathStepData[] paths = new PathStepData[_paths.Length];

			for( int i = 0; i < paths.Length; i++ )
			{
				PathStepData stepData = new PathStepData();
				Vector2Int[] step = new Vector2Int[2];
				CellPaths refStep = _paths[i];

				stepData.StartHexagonSides = refStep.Start.ToStep();
				stepData.EndHexagonSides = refStep.End.ToStep();
				stepData.Path = refStep.Path;
				paths[i] = stepData;
			}

			return paths;
		}
		
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Vector3 position = transform.position + Vector3.up;

			Color[] colors = new[]
			{
				Color.blue,
				Color.cyan,
				Color.green,
				Color.red,
				Color.yellow,
				Color.black,
				Color.grey,
				Color.magenta,
				Color.white,
			};

			int i = 0;

			foreach(CellPaths pathStep in _paths)
			{
				Gizmos.color = colors[i];
				Gizmos.DrawLine(position + pathStep.Start.ToVector(), position);
				Gizmos.DrawLine(position, position + pathStep.End.ToVector());
				i++;
			}
		}
#endif
	}
	
	public struct PathStepData
	{
		public Vector2Int StartHexagonSides;
		public Vector2Int EndHexagonSides;

		public PathCreator Path;
		
		public float Time;
	}

}