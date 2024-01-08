using GameFolder.LevelEditor.LevelEditor;
using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
    public class RightRootPanelLevelEditor : MonoBehaviour
    {
	    [SerializeField] private CreateLevelPanel _createLevelPanel;
	    [SerializeField] private BaseLevelPanel  _basePanel;
	    [SerializeField] private LoadLevelEditorPanel _loadLevelPanel;

	    public CreateLevelPanel CreateLevelPanel => _createLevelPanel;
	    public BaseLevelPanel BaseLevelPanel => _basePanel;
	    public LoadLevelEditorPanel LoadLevelPanel => _loadLevelPanel;
	    
	    public void DeactivateAll()
	    {
		    _createLevelPanel.Deactivate();
		    _basePanel.Deactivate();
		    _loadLevelPanel.Deactivate();
	    }
    }

}