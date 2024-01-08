using System;
using System.Collections.Generic;
using GameFolder.ScriptsFolder.DataFolder;
using GameFolder.ScriptsFolder.DataFolder.CurrencyFolder;
using GameFolder.ScriptsFolder.Services.DataServicesFolder;
using GameFolder.ScriptsFolder.Services.ResourcesFolder;

namespace GameFolder.ScriptsFolder.Services
{
	public class CurrencyServices : IService
	{
		private readonly CurrencySettings _currencySettings;
		private Dictionary<CurrencyType, CurrencyCase> _currencyCase;

		public CurrencyServices(IResourcesLoaderService resourcesLoaderService)
		{
			_currencySettings = resourcesLoaderService.Load<CurrencySettings>(Constants.Currency.CurrencySettingsDataCasePath);

			CurrencySave currencySave = LoadSave();

			CreateData(currencySave);
		}

		private void CreateData(CurrencySave currencySave)
		{
			_currencyCase = new Dictionary<CurrencyType, CurrencyCase>();

			foreach(CurrencySaveCase currencySaveCase in currencySave.Cases)
			{
				CurrencySettingsCase currencySettingsCase = _currencySettings.GetCase(currencySaveCase.CurrencyType);
				CurrencyData currencyData = new()
				{
					Value = currencySaveCase.Value,
				};

				CurrencyCase currencyCase = new CurrencyCase(currencySaveCase.CurrencyType, currencyData, currencySettingsCase.Icon);
				_currencyCase.Add(currencySaveCase.CurrencyType, currencyCase);
			}
		}

		private CurrencySave LoadSave()
		{
			if(DataSaver.TryLoad(out CurrencySave currencySave, Constants.Currency.SavePath))
				return currencySave;

			currencySave = new CurrencySave();
			Array currencyTypeArray = Enum.GetValues(typeof(CurrencyType));
			currencySave.Cases = new CurrencySaveCase[currencyTypeArray.Length];
			int index = 0;

			foreach(CurrencyType currencyType in currencyTypeArray)
			{
				CurrencySettingsCase currencySettingsCase = _currencySettings.GetCase(currencyType);

				CurrencySaveCase currencySaveCase = new()
				{
					CurrencyType = currencyType,
					Value = currencySettingsCase.DefaultValue,
				};

				currencySave.Cases[index] = currencySaveCase;
				index++;
			}
			
			DataSaver.Save(currencySave, Constants.Currency.SavePath);
			
			return currencySave;
		}

		public CurrencyCase GetCase(CurrencyType currencyType) =>
			_currencyCase[currencyType];

		public void Save()
		{
			CurrencySaveCase[] currencySave = new CurrencySaveCase[_currencyCase.Keys.Count];

			int index = 0;
			
			foreach(KeyValuePair<CurrencyType, CurrencyCase> valuePair in _currencyCase)
			{
				CurrencySaveCase saveCase = new()
				{
					CurrencyType = valuePair.Key,
					Value = valuePair.Value.Value,
				};
				
				currencySave[index] = saveCase;
				index++;
			}
			
			DataSaver.Save(currencySave, Constants.Currency.SavePath);
		}
	}


	public struct CurrencySave
	{
		public CurrencySaveCase[] Cases;
	}

	public struct CurrencySaveCase
	{
		public CurrencyType CurrencyType;
		public int Value;
	}
}