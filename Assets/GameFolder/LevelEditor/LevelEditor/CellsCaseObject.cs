using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.ScriptsFolder.LevelEditor
{
	public class CellsCaseObject : MonoBehaviour
	{
		[SerializeField] private TMP_Text _name;
		[SerializeField] private TMP_Text _active;
		[SerializeField] private Button _button;

		[SerializeField] private Image _back;
		
		[SerializeField] private Color _baseColor;
		[SerializeField] private Color _selectedColor;
		
		private string _id;
		
		public event Action<string> TriedSelect;

		private void Awake() =>
			_button.onClick.AddListener(OnSelected);

		private void OnSelected() =>
			TriedSelect?.Invoke(_id);

		public void Initialization(string id, bool active)
		{
			_back.color = _baseColor;
			_id = id;
			_name.text = id;
			_active.text = active ? "Active" : "Not active";
		}

		private void OnDestroy() =>
			_button.onClick.RemoveListener(OnSelected);
		
		public void Deselect() =>
			_back.color = _baseColor;

		public void Select() =>
			_back.color = _selectedColor;
	}
}