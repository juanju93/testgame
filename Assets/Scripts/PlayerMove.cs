using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	//use in Player to allow moving, check "IsTouch" if using mobile, uncheck "IsTouch" if using PC

	private int xPos;
	private float t = 0.1f;
	public bool isTouch;

	// Update is called once per frame
	void Update () {
		if (isTouch == true) {
			//IOS
			if (Input.touchCount > 0 && GameInit.gameStarted == true) {
				GetComponent<Animator> ().SetBool ("IsMoving", true);
				if (Input.GetTouch (0).position.x > Screen.width / 2 && t > 0.075f && xPos < 6) {
					xPos++;
					transform.localScale = new Vector2 (1, 1);
					t = 0.0f;
				}
				if (Input.GetTouch (0).position.x < Screen.width / 2 && t > 0.075f && xPos > -6) {
					xPos--;
					transform.localScale = new Vector2 (-1, 1);
					t = 0.0f;
				}
				t += Time.deltaTime;
			}
		} else {
			//KEYBOARD 
			if (Input.GetButton("Horizontal") && GameInit.gameStarted == true) {
				GetComponent<Animator> ().SetBool ("IsMoving", true);
				if (Input.GetAxis ("Horizontal") > 0 && t > 0.075f && xPos < 6){
					xPos++;
					transform.localScale = new Vector2 (1,1);
					t = 0.0f;
				}
				if (Input.GetAxis ("Horizontal") < 0 && t > 0.075f && xPos > -6){
					xPos--;
					transform.localScale = new Vector2 (-1,1);
					t = 0.0f;
				}
				t += Time.deltaTime;
			}
		}
	MovePlayer ();
	}

	void MovePlayer(){
		if (Input.GetButton ("Horizontal") == false && isTouch == false) {
			GetComponent<Animator> ().SetBool ("IsMoving", false);
		} 
		if (Input.touchCount < 1 && isTouch == true) {
			GetComponent<Animator> ().SetBool ("IsMoving", false);
		} 
		Vector2 playerPos = gameObject.transform.position;
		playerPos.x = xPos;
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, playerPos, 10 * Time.deltaTime);
	}
}