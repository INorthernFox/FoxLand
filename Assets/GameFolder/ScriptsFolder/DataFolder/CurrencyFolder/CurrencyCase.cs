using System;
using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder.CurrencyFolder
{
	public class CurrencyCase
	{
		private readonly CurrencyData _currencyData;
		private readonly Sprite _icon;

		public event Action Updated;

		public int Value => _currencyData.Value;
		public CurrencyType CurrencyType { get; private set; }

		public CurrencyCase(CurrencyType currencyType, CurrencyData currencyData, Sprite icon)
		{
			_icon = icon;
			_currencyData = currencyData;
			CurrencyType = currencyType;
		}

		public string GetTextValue() =>
			_currencyData.Value.ToString();

		public Sprite GetIcon() =>
			_icon;

		public void TryAddValue(int value)
		{
			try
			{
				if(value < 0)
					throw new ArgumentException($"[{nameof(CurrencyCase)}] Value {value} less than zero");

				_currencyData.Value += value;
				Updated?.Invoke();
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		public bool TryRemoveValue(int value)
		{
			try
			{
				if(value < 0)
					throw new ArgumentException($"[{nameof(CurrencyCase)}] Value {value} less than zero");

				if(value > Value)
					return false;

				_currencyData.Value -= value;
				Updated?.Invoke();
				return true;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
				return false;
			}
		}
	}


}