//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//
//public class DataManagement : MonoBehaviour {
//
//	public static DataManagement datamanagement;
//
//	//variables
//	public int tokensHighScore;
//
//	void Awake (){
//		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
//	}
//
//	public void SaveData(){
//		BinaryFormatter binForm = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "gameInfo.dat");
//		gameData data = new gameData ();
//		data.tokensHighScore = tokensHighScore;
//		binForm.Serialize (file, data);
//		file.Close ();
//	}
//
//	public void LoadData(){
//		if (File.Exists (Application.persistentDataPath + "gameInfo.dat")) {
//			BinaryFormatter binForm = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "gameInfo.dat", FileMode.Open);
//			gameData data = (gameData)binForm.Deserialize (file);
//			file.Close ();
//			tokensHighScore = data.tokensHighScore;
//		}
//	}
//}
//
//[Serializable]
//class gameData {
//	public int tokensHighScore;
//		}
//}