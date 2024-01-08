using System;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace GameFolder.ScriptsFolder.DataFolder
{
	[CreateAssetMenu(fileName = "GameConstantsSettingsValue", menuName = "Fox/Settings/GameConstantsSettingsValue", order = 0)]
	public class GameConstantsSettingsValue : ScriptableObject
	{
		[BoxGroup("Field"), SerializeField] private FieldSettings _fieldSettings;
		[BoxGroup("Camera"), SerializeField] private CameraSettings _cameraSettings;
		[BoxGroup("Camera"), SerializeField] private TextSettings _text;

		public FieldSettings FieldSettings => _fieldSettings;
		public CameraSettings CameraSettings => _cameraSettings;
		public TextSettings Text => _text;

		private void OnValidate()
		{
			_text = Text.OnValidate();
		}
	}

	[Serializable]
	public struct TextSettings
	{
		[SerializeField] private string _levelInfoUISample;
		[SerializeField] private StringType _stringType;
		[SerializeField, TextArea(3, 8)] private string _example;

		public LevelInfoUISampleConvector LevelInfoUISampleConvector => new LevelInfoUISampleConvector(_levelInfoUISample, _stringType);

		public TextSettings OnValidate()
		{
			string result = LevelInfoUISampleConvector.GetText(1);
			result = "Use {Value} to indicate where the level value should be entered | Use \\n for line break\n" + result;
			_example = result;
			return this;
		}

		public enum StringType
		{
			Base = 0,
			NumberWithZeros = 1,
		}
	}

	public readonly struct LevelInfoUISampleConvector
	{
		private readonly string _levelInfoUISample;
		private readonly TextSettings.StringType _stringType;

		public LevelInfoUISampleConvector(string levelInfoUISample, TextSettings.StringType stringType)
		{
			_levelInfoUISample = levelInfoUISample;
			_stringType = stringType;
		}

		public string GetText(int level)
		{
			string value = level.ToString();

			if(_stringType == TextSettings.StringType.NumberWithZeros)
			{
				while(value.Length < 3)
					value = "0" + value;
			}

			string result = _levelInfoUISample.Replace("{Value}", value);
			result = result.Replace("\\n", "\n");

			return result;
		}
	}

	[Serializable]
	public struct CameraSettings
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private Vector3 _defaultPosition;
		[SerializeField] private Vector3 _defaultRotation;

		public Camera Prefab => _camera;
		public Vector3 DefaultRotation => _defaultRotation;
		public Vector3 DefaultPosition => _defaultPosition;
	}

	[Serializable]
	public struct FieldSettings
	{
		[SerializeField] private Vector2 _cellSize;
		[SerializeField] private Vector2 _gap;
		[SerializeField] private float _hexagonDisplacementCoefficient;
		[SerializeField] private Material _projectionMaterial;
		[SerializeField] private float _yCellPosition;
		[SerializeField] private FieldTypeSettings[] _fieldTypeSettings;

		public Vector2 CellSize => _cellSize;
		public Vector2 Gap => _gap;
		public float HexagonDisplacementCoefficient => _hexagonDisplacementCoefficient;
		public Material ProjectionMaterial => _projectionMaterial;
		public float YCellPosition => _yCellPosition;

		public IReactiveCollection<FieldTypeSettings> FieldTypeSettings => _fieldTypeSettings.ToReactiveCollection();
	}

	[Serializable]
	public struct FieldTypeSettings
	{
		[SerializeField] private string _name;
		[SerializeField] private Vector2Int _size;
		[SerializeField] private Vector3 _cameraPosition;
		[SerializeField] private Vector3 _cameraRotation;

		public string Name => _name;
		public Vector2Int Size => _size;
		public Vector3 CameraPosition => _cameraPosition;
		public Vector3 CameraRotation => _cameraRotation;
	}
}