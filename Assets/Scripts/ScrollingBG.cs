using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour {

	public float speed = 0.5f;
	Vector2 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		
	}

	// Update is called once per frame
	void Update () {
		float newPos = Mathf.Repeat (Time.time * speed, 11); //how far away upper pic is from original bg
		transform.position = startPos + Vector2.down * newPos;
	}
}
