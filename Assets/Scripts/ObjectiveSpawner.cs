using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawner : MonoBehaviour {

	public GameObject token;
	public GameObject[] trees;
	private float tokenTimer = 2.0f;
	private float treeTimer = 2.0f;

	// Update is called once per frame
	void Update () {
		tokenTimer -= Time.deltaTime;
		treeTimer -= Time.deltaTime;
		if (tokenTimer < 0) {
			SpawnToken ();
		}
		if (treeTimer < 0) {
			SpawnTree ();
		}
	}

	void SpawnToken () {
		GameObject tok = Instantiate (token, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		tokenTimer = Random.Range (0.5f, 2.5f);	
	}

	void SpawnTree(){
		GameObject tre = Instantiate (trees[(Random.Range(0, trees.Length))],new Vector2(Random.Range(-6, 6), 10),Quaternion.identity) as GameObject;
		treeTimer = Random.Range(0.5f, 2.5f);
		tre.GetComponent<Rigidbody2D> ().gravityScale = Random.Range (1, 3);
	}
}