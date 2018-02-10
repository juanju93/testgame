using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawner : MonoBehaviour {

	public GameObject cam;

	public GameObject player;
	public GameObject token;
	public GameObject[] trees;
	public GameObject bigTree;
	public GameObject[] debrees;


	private float tokenTimer = 2.0f;
	private float treeTimer = 2.0f;
	private float bigTreeTimer = 5.0f;

	//public AudioClip bigTreeFalling;

	// Update is called once per frame
	void Update () {
		tokenTimer -= Time.deltaTime;
		treeTimer -= Time.deltaTime;
		bigTreeTimer -= Time.deltaTime;
		if (tokenTimer < 0) {
			SpawnToken ();
		}
		if (treeTimer < 0) {
			SpawnTree ();
		}
		if (bigTreeTimer < 0) {
			StartCoroutine (SpawnBigTree ());
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

	IEnumerator SpawnBigTree ()
	{
		bigTreeTimer = 1000;
		Vector2 spawnSpot;
		int debreeTimer = (Random.Range (20, 30));
		int spwnOnPlayerPer = (Random.Range (0, 100));
		if (spwnOnPlayerPer < 50) {
			spawnSpot = new Vector2 (Random.Range (-6, 6), 20);
		} else {
			spawnSpot = new Vector2 (player.transform.position.x, 20);
		}
		while (debreeTimer > 0) {
			debreeTimer--;
			float t = Random.Range (0.005f, 0.1f);
			yield return new WaitForSeconds (t);
			Debug.Log ("Spawned");
			GameObject debree = Instantiate (debrees [(Random.Range (0, debrees.Length))], new Vector2 (spawnSpot.x + Random.Range (-1.0f, 1.0f), spawnSpot.y - 10), Quaternion.identity) as GameObject;
		}
		Debug.Log ("Complete");
		iTween.ShakePosition (cam, new Vector3 (0.1f, 0.1f, 0.1f), 1);
		yield return new WaitForSeconds (1);
		Instantiate (bigTree, spawnSpot, Quaternion.identity);
		//player.GetComponent<AudioSource> ().PlayOneShot (bigTreeFalling, 0.5f);
		bigTreeTimer = Random.Range (15.0f, 45.0f);
	}
}