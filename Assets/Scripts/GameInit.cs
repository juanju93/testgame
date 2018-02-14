using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

	public static bool gameStarted;
	public GameObject cam;
	public GameObject hearts;
	public GameObject belleLogo;
	public GameObject crownRankingLogo;
	public GameObject mainLogo;
	public GameObject speechBubble;
	public bool canStartGame = false;


	void Awake () {
		Application.targetFrameRate = 60;
		PlayerCollide.health = 3;
		PlayerCollide.tokens = 0;
		gameStarted = false;
	}

	void Start (){
		speechBubble.SetActive (true);
		hearts.SetActive (true);
		belleLogo.SetActive (true);
		crownRankingLogo.SetActive (true);
		mainLogo.SetActive (true);
	}

	void Update () {

		if (canStartGame == true && (Input.touchCount > 0 || Input.GetButton ("Horizontal"))) {
			gameStarted = true;
		}
		if (gameStarted == true){
			speechBubble.SetActive (false);
			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (0, 0, -10), 2 * Time.deltaTime);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 5, 5 * Time.deltaTime);
		//IOS
		if (Input.touchCount > 0) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			Vector2 touchPos = new Vector2 (worldPos.x, worldPos.y);
			if (gameStarted == false && mainLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				StartCoroutine (StartGame ());
			}
			if (gameStarted == false && belleLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				Application.OpenURL("http://bellegames.net/belledot/");
			}	
			if (gameStarted == false && crownRankingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				//Load Gamecenter
			}
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
			}	
			if (gameStarted == false && crownRankingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				//Load Gamecenter
				}
			}
		}
	}

		IEnumerator StartGame(){
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime;
			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (1.8f, -1.45f, -10), 0.1f);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 2, 0.1f);
			speechBubble.SetActive (true);
			mainLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
			belleLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (-15, 0), 0.1f);
			crownRankingLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (15, 0), 0.1f);
			yield return new WaitForSeconds (0.02f);
		}
		Destroy (mainLogo);
		Destroy (belleLogo);
		Destroy (crownRankingLogo);
		hearts.SetActive (true);
		canStartGame = true;
	}
}