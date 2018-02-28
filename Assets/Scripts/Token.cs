using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

	//use with coins, make them spin and fade away.

	private bool canRotate = false;
	private float lifeTimer = 7.0f;


	void Update () {
		lifeTimer -= Time.deltaTime;
		if (canRotate == true) {
			transform.Rotate (Vector2.up * 200 * Time.deltaTime);
		}
		if (lifeTimer < 0) {
			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (GetComponent<SpriteRenderer> ().color, new Color (1, 1, 1, 0), 2 * Time.deltaTime);
			if (GetComponent<SpriteRenderer> ().color.a < 0.1) {
				Destroy (gameObject);
			}
		}	
	}

	void OnCollisionEnter2D(Collision2D col){
		canRotate = true;
	}
}
