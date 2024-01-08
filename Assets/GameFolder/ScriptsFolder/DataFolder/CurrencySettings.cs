using System;
using System.Collections.Generic;
using System.Linq;
using GameFolder.ScriptsFolder.DataFolder.CurrencyFolder;
using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder
{
	[CreateAssetMenu(menuName = "Fox/Settings/Currency/Settings", fileName = Constants.Currency.CurrencySettingsName, order = 0)]
	public class CurrencySettings : ScriptableObject
	{
		[SerializeField] private CurrencySettingsCase[] _currencySettings;

		public CurrencySettingsCase GetCase(CurrencyType currencyType) =>
			_currencySettings.First(p => p.CurrencyType == currencyType);
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			List<CurrencySettingsCase> cases = _currencySettings.ToList();

			foreach(CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
			{
				if(cases.Any(p => p.CurrencyType == currencyType))
					continue;

				CurrencySettingsCase settingsCase = new()
				{
					Name = currencyType.ToString(),
					DefaultValue = 0,
					Icon = null,
					CurrencyType = currencyType,
				};

				cases.Add(settingsCase);
			}

			cases = cases.OrderBy(p => (int) p.CurrencyType).ToList();

			for( int index = 0; index < cases.Count; index++ )
			{
				CurrencySettingsCase settingsCase = cases[index];

				settingsCase.Name = settingsCase.CurrencyType.ToString();
				cases[index] = settingsCase;
			}

			_currencySettings = cases.ToArray();
		}
  #endif
	}
	
	[Serializable]
	public struct CurrencySettingsCase
	{
		public string Name;
		public CurrencyType CurrencyType;
		public int DefaultValue;
		public Sprite Icon;
	}
}