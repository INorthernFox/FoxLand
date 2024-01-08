using System;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.GameSessionFolder
{
	public interface IInputService : IService
	{
		event Action Started;
		event Action Stopped;
		event Action PositionChanged;
		Vector2 Position { get; }
		Vector2 Delta { get; }
		void Enable();
		void Disable();
	}
}