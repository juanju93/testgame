using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

	private bool canRotate = false;


	void Update () {
		if (canRotate == true) {
			transform.Rotate (Vector2.up * 200 * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		canRotate = true;
	}
}
