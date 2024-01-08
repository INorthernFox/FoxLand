using GameFolder.ScriptsFolder.DataFolder.CurrencyFolder;
using GameFolder.ScriptsFolder.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class CurrencyPanel : MonoBehaviour
	{
		[SerializeField] private CurrencyType _currencyType;
		[SerializeField] private TMP_Text _valueText;
		[SerializeField] private Image _icon;

		private CurrencyCase _currencyCase;

		public void Initialization(CurrencyServices currencyServices)
		{
			_currencyCase = currencyServices.GetCase(_currencyType);
			_icon.sprite = _currencyCase.GetIcon();
			_currencyCase.Updated += OnCurrencyUpdated;
			SetValue();
		}

		private void SetValue()
		{
			_valueText.text = _currencyCase.GetTextValue();
		}

		private void OnCurrencyUpdated()
		{
			SetValue();
		}
	}
}