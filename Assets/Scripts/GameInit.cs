using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

	public static bool gameStarted;
	public GameObject cam;


	void Awake () {
		PlayerCollide.health = 3;
		PlayerCollide.tokens = 0;
		gameStarted = false;
	}

	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			gameStarted = true;
		}
		if (gameStarted == false) {
			cam.gameObject.transform.position = new Vector3 (2.15f, -1.45f, -10);
			cam.GetComponent<Camera> ().orthographicSize = 2;
		} else {
			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (0, 0, -10), 2 * Time.deltaTime);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 5, 5 * Time.deltaTime);
		
		}
	}
}
