using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.UIFolder.ScriptsFolder
{
	[RequireComponent(typeof(Button))]
	public abstract class BaseButton : MonoBehaviour
	{
		[SerializeField] private Button _button;

		public event Action OnClicked;
		
		private void Awake()
		{
			_button.onClick.AddListener(OnClick);
			OnAwake();
		}

		public void SetInteractable(bool interactable) =>
			_button.interactable = interactable;

		public void Activate()
		{
			SetInteractable(true);
			gameObject.SetActive(true);
			OnActivate();
		}	
		
		protected virtual void OnActivate(){}
		
		public void Deactivate()
		{
			gameObject.SetActive(false);
			OnDeactivate();
		}
		
		protected virtual void OnDeactivate(){}
		
		protected virtual void OnAwake(){}

		protected virtual void OnClick()
		{
			SetInteractable(false);
			OnClicked?.Invoke();
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(OnClick);
			OnDestroyed();
		}
		protected virtual void OnDestroyed(){}

		private void OnValidate() =>
			_button ??= GetComponent<Button>();
	}
}