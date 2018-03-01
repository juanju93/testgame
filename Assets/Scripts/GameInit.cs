using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GameInit : MonoBehaviour {

	//use this in ~SCRIPTS gameobject and put objects in slots

	public static bool gameStarted;
    public bool canStartGame = false;
    public GameObject cam;
	public GameObject hearts;
	public GameObject belleLogo;
	//public GameObject crownRankingLogo; //for future highscores
	public GameObject mainLogo;
	public GameObject speechBubble;
	//public GameObject menuBottomText;
	private bool showScore = false;
	//private string LeaderBoard1ID = "Kuura_Highscore_LeaderBoard_1:ID";
	


	void Awake () {
		//DataManagement.datamanagement.LoadData ();
		Application.targetFrameRate = 60;
		PlayerCollide.health = 3;
		PlayerCollide.tokens = 0;
		Time.timeScale = 1.0f;
		gameStarted = false;
	}

	void Start (){
		//Social.localUser.Authenticate (ProcessAuthenticatoon);
		//menuBottomText.GetComponent<Text> ().text = "High Score " + DataManagement.datamanagement.tokensHighScore.ToString ();
		speechBubble.SetActive (false);
		hearts.SetActive (false);
		belleLogo.SetActive (true);
		//crownRankingLogo.SetActive (true);
		mainLogo.SetActive (true);
	}

	void Update ()
	{

		if (canStartGame == true && (Input.touchCount > 0 || Input.GetButton ("Horizontal"))) {
			gameStarted = true;
		}
		if (gameStarted == true) {
			speechBubble.SetActive (false);
			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (0, 0, -10), 2 * Time.deltaTime);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 5, 5 * Time.deltaTime);
		}
		//IOS
		if (Input.touchCount > 0) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (worldPos.x, worldPos.y);
			if (gameStarted == false && mainLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				StartCoroutine (StartGame ());
			}
			if (gameStarted == false && belleLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				Application.OpenURL ("http://bellegames.net/belledot/"); //this makes icon clickable and opens website!
			}
			//if (gameStarted == false && crownRankingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {  
				//Social.ReportScore ((long)DataManagement.datamanagement.tokensHighScore, LeaderBoard1ID, HighScoreCheck);
				//Social.ShowLeaderboardUI ();
			//}
		}

		//PC
		if (Input.GetMouseButtonDown(0)) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePos = new Vector2 (worldPos.x, worldPos.y);
			if (gameStarted == false && mainLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				StartCoroutine (StartGame ());
			}
			if (gameStarted == false && belleLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				Application.OpenURL("http://bellegames.net/belledot/");
			//}	
			//if (gameStarted == false && crownRankingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				//Social.ReportScore ((long)DataManagement.datamanagement.tokensHighScore, LeaderBoard1ID, HighScoreCheck);
				//Social.ShowLeaderboardUI ();
				//}
			}
		}

//	//gamecenter
//	void ProcessAuthenticatoon (bool succes){
//		if (succes == true) {
//			Debug.Log ("Authenticated,Checking achievements");
//			Social.LoadAchievements (ProcessLoadedAchievements);
//		}else{
//			Debug.Log ("failed authentication");
//		}
//	}
//
//	void ProcessLoadedAchievements (IAchievement[] achievements){
//		if (achievements.Length == 0){
//			Debug.Log ("Error, no achievements found");
//		}else{
//			Debug.Log ("Got " + achievements.Length + " achievements");
//		}
//	}
//
//	public static void HighScoreCheck (bool results){
//		if (results == true) {
//			Debug.Log ("Score submission succesful");
//		}else{
//			Debug.Log ("score submission failed");
//		}
//	}

	//public void SwapBottomMenuText(){
		//if (showScore == true) {
			//menuBottomText.GetComponent<Text> ().text = "Total Tokens " + DataManagement.datamanagement.totalCollectedTokens.ToString ();
			//showScore = false;

		//} else {
			//menuBottomText.GetComponent<Text> ().text = "High Score " + DataManagement.datamanagement.tokensHighScore.ToString ();
			//showScore = true;
		//}
	}
		IEnumerator StartGame ()
	{
		//if (DataManagement.datamanagement.tokensHighScore < 1000) {
			float t = 0;
			while (t < 1) {
				t += Time.deltaTime;
				cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (1.8f, -1.45f, -10), 0.1f);
				cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 2, 0.1f);
				speechBubble.SetActive (true);
				//menuBottomText.SetActive (true);
				mainLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
				belleLogo.transform.position = Vector2.Lerp (belleLogo.transform.position, new Vector2 (-15, 0), 0.1f);
				//crownRankingLogo.transform.position = Vector2.Lerp (crownRankingLogo.transform.position, new Vector2 (15, 0), 0.1f);
				yield return new WaitForSeconds (0.02f);
		
//		} else {
//			float t = 0;
//			while (t < 1) {
//				t += Time.deltaTime;
//				//menuBottomText.SetActive (false);
//				hearts.SetActive (true);
//				canStartGame = true;
//				mainLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
//				belleLogo.transform.position = Vector2.Lerp (belleLogo.transform.position, new Vector2 (-15, 0), 0.1f);
//				crownRankingLogo.transform.position = Vector2.Lerp (crownRankingLogo.transform.position, new Vector2 (15, 0), 0.1f);
//				yield return new WaitForSeconds (0.01f);
			}

			Destroy (mainLogo);
			Destroy (belleLogo);
			//Destroy (crownRankingLogo);
			hearts.SetActive (true);
			canStartGame = true;
	}
}