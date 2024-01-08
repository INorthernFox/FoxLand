using UnityEngine;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public  class MainRootPanelLevelEditor : MonoBehaviour
	{
		[SerializeField] private RightRootPanelLevelEditor _right;
		[SerializeField] private LeftRootPanelLevelEditor _left;
		[SerializeField] private LowerRootPanelLevelEditor _lower;

		public RightRootPanelLevelEditor Right => _right;
		public LeftRootPanelLevelEditor Left => _left;
		public LowerRootPanelLevelEditor Lower => _lower;

		public void Initialization()
		{
			
		}
	}
}