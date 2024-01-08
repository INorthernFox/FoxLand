using GameFolder.ScriptsFolder.LevelEditor;
using GameFolder.ScriptsFolder.Services.GameSessionFolder;

namespace GameFolder.LevelEditor.LevelEditor
{
	public class LevelEditorRoot
	{
		private readonly CellCacheService _cellCacheService;
		private readonly MainRootPanelLevelEditor _mainRootPanelLevelEditor;
		private readonly CameraService _cameraService;

		private string _currentSelectedCellReference;

		public LevelEditorRoot(CellCacheService cellCacheService, MainRootPanelLevelEditor mainRootPanelLevelEditor, CameraService cameraService)
		{
			_cellCacheService = cellCacheService;
			_mainRootPanelLevelEditor = mainRootPanelLevelEditor;
			_cameraService = cameraService;
			_cameraService.CreateCamera(null);
			CellsPanel();
		}

		private async void CellsPanel()
		{
			CellsLevelEditorPanel cellsPanel = _mainRootPanelLevelEditor.Left.CellsPanel;
			await cellsPanel.Initialization(_cellCacheService);
			cellsPanel.NewCellSelected += CellsPanelOnNewCellSelected;
		}

		private void CellsPanelOnNewCellSelected(string id) =>
			_currentSelectedCellReference = id;
	}
}