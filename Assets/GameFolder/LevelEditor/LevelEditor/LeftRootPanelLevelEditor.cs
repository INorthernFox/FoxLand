using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class LeftRootPanelLevelEditor : MonoBehaviour
	{
		[SerializeField] private CellsLevelEditorPanel _cellsPanel;

		public CellsLevelEditorPanel CellsPanel => _cellsPanel;
		
		public void DeactivateAll() =>
			_cellsPanel.Deactivate();
	}
}