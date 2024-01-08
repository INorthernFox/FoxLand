using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Services.DataServicesFolder
{
	public static class DataSaver
	{
		public static void Save<T>(T data, string path)
		{
			string jsonData = JsonConvert.SerializeObject(data);

			string directoryPath = Path.GetDirectoryName(path);

			if(!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);

			using(FileStream stream = new FileStream(path, FileMode.Create))
			{
				using(StreamWriter writer = new StreamWriter(stream))
					writer.Write(jsonData);
			}
		}

		public static bool TryLoad<T>(out T data, string path)
		{
			if(File.Exists(path))
			{
				try
				{
					string jsonData = File.ReadAllText(path);
					data = JsonConvert.DeserializeObject<T>(jsonData);
					return true;
				}
				catch (Exception e)
				{
					// ignored
				}

			}
		
			Debug.LogWarning($"No saved data found at path: {path}.");
			data = default( T );
			return false;
		}
	}
}