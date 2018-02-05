using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	private int xPos;
	private float t = 0.1f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Horizontal")) {
			GetComponent<Animator> ().SetBool ("IsMoving", true);
			if (Input.GetAxis ("Horizontal") > 0 && t > 0.05f && xPos < 6){
				xPos++;
				transform.localScale = new Vector2 (1,1);
				t = 0.0f;
			}
			if (Input.GetAxis ("Horizontal") < 0 && t > 0.05f && xPos > -6){
				xPos--;
				transform.localScale = new Vector2 (-1,1);
				t = 0.0f;
			}
			t += Time.deltaTime;
		}
		MovePlayer ();
	}

	void MovePlayer(){
		if (Input.GetButton ("Horizontal")) {
			GetComponent<Animator> ().SetBool ("IsMoving", false);
		}
		Vector2 playerPos = gameObject.transform.position;
		playerPos.x = xPos;
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, playerPos, 10 * Time.deltaTime);
	}
}
