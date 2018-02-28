//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//
////make empty gameobject -Data- and put this script for saving high scores
////saving works on pc build but something wrong in android so disabled from other scripts now
//
//public class DataManagement : MonoBehaviour {
//
//	public static DataManagement datamanagement;
//
//	//variables
//	public int tokensHighScore;
//	public int totalCollectedTokens;
//
//	void Awake (){
//		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
//		if (datamanagement == null) {
//			DontDestroyOnLoad (gameObject);
//			datamanagement = this;
//		} 
//		else if (datamanagement != this)
//		{
//			Destroy (gameObject);
//		}
//	}
//	public void SaveData() {
//		BinaryFormatter binForm = new BinaryFormatter ();
//		FileStream file = File.Create (Application.persistentDataPath + "/gameInfo.dat");
//		gameData data = new gameData ();
//		data.tokensHighScore = tokensHighScore;
//		data.totalCollectedTokens = totalCollectedTokens;
//		binForm.Serialize (file, data);
//		file.Close ();
//	}
//
//	public void LoadData() {
//		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) {
//			BinaryFormatter binForm = new BinaryFormatter ();
//			FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
//			gameData data = (gameData)binForm.Deserialize (file);
//			file.Close ();
//			tokensHighScore = data.tokensHighScore;
//			totalCollectedTokens = data.totalCollectedTokens;
//		}
//	}
//}
//
//[Serializable]
//class gameData {
//	public int tokensHighScore;
//	public int totalCollectedTokens;		
//}