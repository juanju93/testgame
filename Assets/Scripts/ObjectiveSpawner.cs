using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawner : MonoBehaviour {

	//use in ~SCRIPTS gameobject and make stuff spawn.

	public GameObject cam;
	public GameObject player;
	public GameObject token;
	public GameObject bigToken;
	public GameObject[] trees;
	public GameObject bigTree;
	public GameObject[] debrees;
	public GameObject gapEmpty;
	public GameObject invinclibilityToken;
	public GameObject heartToken;
	private float tokenTimer;
	private float treeTimer;
	private float bigTreeTimer;
	private float gapTreesTimer;
	private float bigTokenTimer;
	private float invincibilityTokenTimer;
	private float hearthTokenTimer;
	public AudioSource bigTreeFallingSound;

	void Start (){
		bigTreeTimer = Random.Range (20.0f, 40.0f); //random times between when objects are spawning.
		treeTimer = Random.Range (1.0f, 5.0f);
		tokenTimer = Random.Range (1.0f, 2.0f);
		gapTreesTimer = Random.Range (5.0f, 10.0f);
		bigTokenTimer = Random.Range (30.0f, 90.0f);
		invincibilityTokenTimer = Random.Range (30.0f, 80.0f);
		hearthTokenTimer = Random.Range (30.0f, 90.0f);
	}

	// Update is called once per frame
	void Update () {
		if (GameInit.gameStarted == true) {
			tokenTimer -= Time.deltaTime;
			treeTimer -= Time.deltaTime;
			bigTreeTimer -= Time.deltaTime;
			gapTreesTimer -= Time.deltaTime; 
			bigTokenTimer -= Time.deltaTime; 
			invincibilityTokenTimer -= Time.deltaTime;
			hearthTokenTimer -= Time.deltaTime;

			if (tokenTimer < 0) {
				SpawnToken ();
			}
			if (bigTokenTimer < 0) {
				SpawnBigToken ();
			}
			if (invincibilityTokenTimer < 0) {
				SpawnInvincibilityToken ();
			}
			if (hearthTokenTimer < 0) {
				SpawnHearthToken ();
			}
			if (treeTimer < 0) {
				SpawnTree ();
			}
			if (bigTreeTimer < 0) {
				StartCoroutine (SpawnBigTree ());
			}
			if (gapTreesTimer < 0) {
				StartCoroutine (SpawnGapTrees ());
			}
		}
	}
	void SpawnToken () {
		GameObject tok = Instantiate (token, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		tokenTimer = Random.Range (0.5f, 2.5f);	
	}
	void SpawnBigToken () {
		GameObject bigTok = Instantiate (bigToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		bigTokenTimer = Random.Range (30.0f, 90.0f);
	}
	void SpawnHearthToken(){
		GameObject hearthTok = Instantiate (heartToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		hearthTokenTimer = Random.Range (30.0f, 90.0f);
	}
	void SpawnInvincibilityToken() {
		GameObject inviTok = Instantiate (invinclibilityToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		invincibilityTokenTimer = Random.Range (15.0f, 40.0f);	
	}

	void SpawnTree(){
		GameObject tre = Instantiate (trees[(Random.Range(0, trees.Length))],new Vector2(Random.Range(-7, 7), 10),Quaternion.identity) as GameObject;
		if (PlayerCollide.tokens < 25) {
			treeTimer = Random.Range (1.5f, 5.0f);
		}
		if (PlayerCollide.tokens > 24 && PlayerCollide.tokens < 50) {
			treeTimer = Random.Range (1.0f, 4.0f);
		}
		if (PlayerCollide.tokens > 49 && PlayerCollide.tokens < 75) {
			treeTimer = Random.Range (0.8f, 3.0f);
		}
		if (PlayerCollide.tokens > 74 && PlayerCollide.tokens < 100) {
			treeTimer = Random.Range (0.5f, 1.5f);
		}
		if (PlayerCollide.tokens > 99) {
			treeTimer = Random.Range (0.2f, 1.0f);
		}
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
			bigTreeFallingSound.Play ();
			debreeTimer--;
			float t = Random.Range (0.005f, 0.1f);
			yield return new WaitForSeconds (t);
			//Debug.Log ("Spawned");
			GameObject debree = Instantiate (debrees [(Random.Range (0, debrees.Length))], new Vector2 (spawnSpot.x + Random.Range (-1.0f, 1.0f), spawnSpot.y - 10), Quaternion.identity) as GameObject;
		}
		//Debug.Log ("Complete");
		iTween.ShakePosition (cam, new Vector3 (0.1f, 0.1f, 0.1f), 1);
		yield return new WaitForSeconds (1);
		Instantiate (bigTree, spawnSpot, Quaternion.identity);
		bigTreeTimer = Random.Range (15.0f, 45.0f);
	}
	IEnumerator SpawnGapTrees(){
		bigTreeTimer = 1000;
		treeTimer = 1000;
		tokenTimer = 1000;
		gapTreesTimer = 1000;
		int numTreeSets = Random.Range (1,3);
		for (int v = 0; v < numTreeSets; v++) {
			yield return new WaitForSeconds (1);
			int xPos = -10;
			int GapXpos = Random.Range (-6, 6);
			GameObject gapTreeGroup = new GameObject ();
			gapTreeGroup.name = "Gap Tree Group";
			//put in audio stuff
			for (int i = 0; i < 18; i++) {
				GameObject tre = Instantiate (trees [(Random.Range (0, trees.Length))], new Vector2 (xPos, 5), Quaternion.identity) as GameObject;
				//add some more audio
				Destroy (tre.GetComponent<Rigidbody2D> ());
				if (xPos != GapXpos) {
					xPos++;
				} else {
					GameObject e = Instantiate (gapEmpty, new Vector2 (xPos + 1, 5), Quaternion.identity) as GameObject;
					xPos = xPos + 3;
					e.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				}
				tre.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				yield return new WaitForSeconds (0.03f);
			}
			float randomTime = Random.Range (0.5f, 3.0f);
			iTween.ShakePosition (gapTreeGroup, new Vector2 (0.2f, 0.2f), randomTime);
			yield return new WaitForSeconds (randomTime);
			//foreach (Rigidbody2D rb in gapTreeGroup.GetComponentsInChildren<Rigidbody2D>(true)) {
			gapTreeGroup.AddComponent<Rigidbody2D> ();
			gapTreeGroup.GetComponent<Rigidbody2D> ().gravityScale = 10;
			gapTreeGroup.gameObject.layer = 9;
			gapTreeGroup.gameObject.tag = "log";
			gapTreeGroup.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
			gapTreeGroup.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			gapTreeGroup.AddComponent<AudioSource> ();
		}
		bigTreeTimer = Random.Range (15.0f, 45.0f);
		treeTimer = Random.Range (2.0f, 5.0f);
		tokenTimer = Random.Range (0.5f, 1.0f);
		gapTreesTimer = Random.Range (30.0f, 75.0f);
	}
}