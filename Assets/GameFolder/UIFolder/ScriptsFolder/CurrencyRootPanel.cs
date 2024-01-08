using GameFolder.ScriptsFolder.Services;
using UnityEngine;

namespace GameFolder.UIFolder.ScriptsFolder
{
	public class CurrencyRootPanel : MonoBehaviour
	{
		[SerializeField] private CurrencyPanel[] _currencyPanels;
		
		public void Initialization(CurrencyServices currencyServices)
		{
			foreach(CurrencyPanel currencyPanel in _currencyPanels)
				currencyPanel.Initialization(currencyServices);
		}
	}
}