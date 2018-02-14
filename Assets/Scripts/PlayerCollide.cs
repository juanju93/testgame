﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour {

	public GameObject cam;
	public GameObject tokenUI;
	public GameObject token;
	public static int health = 3;
	public static int tokens;
	public AudioClip collectToken;
	public AudioClip pain;
	private bool canTakeDamage = true;
	public AudioSource heartSound;
	public AudioSource invicibilitySound;

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			StartCoroutine (GameOver ());
		}
	}

	void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.tag == "token") {
			GetComponent<AudioSource> ().PlayOneShot (collectToken, 0.5f);
			tokens++;
			tokenUI.GetComponent<Text> ().text = (tokens.ToString ());
			Destroy (trig.gameObject);

		}

		if(trig.gameObject.tag == "bigToken"){
			GetComponent<AudioSource> ().PlayOneShot (collectToken, 0.5f); //make different audio
			StartCoroutine (SpawnExplosionOfTokensAfterBigToken());
			Destroy (trig.gameObject);
		}
		if (trig.gameObject.tag == "log"){
			TakeDamage ();
		}
		if (trig.gameObject.tag == "biglog") {
			TakeBigDamage ();
		}
		if (trig.gameObject.tag == "gapempty") {
			StartCoroutine (SpawnExplosionOfTokens ());
		}
		if(trig.gameObject.tag == "invincibility"){
			StartCoroutine (Invincibility ());
			invicibilitySound.Play ();
			Destroy (trig.gameObject);
		}
		if(trig.gameObject.tag == "heart"){
			GainHeart();
			heartSound.Play ();
			Destroy (trig.gameObject);
		}
	}

	void TakeDamage(){
		if (canTakeDamage == true) {
			iTween.ShakePosition (cam, new Vector3 (0.2f, 0.2f, 0.2f), 1);   //iTween unity asset 
			GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f);
			health--;
			StartCoroutine (CantGetHurt());
		}
	}

	void TakeBigDamage(){
		if (canTakeDamage == true) {
			iTween.ShakePosition (cam, new Vector3 (0.4f, 0.4f, 0.4f), 1);   //iTween unity asset 
			GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f); //make different audio
			health = health - 3;
		}
	}

	void GainHeart(){
		if(health < 3){
			health++;
		}
	}

	IEnumerator
	SpawnExplosionOfTokens(){
		yield return new WaitForSeconds (0.3f);
		//play some audio
		int explodingTokens = Random.Range(5,10);
		for (int i = 0; i < explodingTokens; i++) {
			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (5.0f, 10.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (3.0f, 10.0f), ForceMode2D.Impulse);
			}else{
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (3.0f, 10.0f), ForceMode2D.Impulse);
			}
		}
		yield return new WaitForSeconds (1);
	}

	IEnumerator
	SpawnExplosionOfTokensAfterBigToken(){
		//play some audio
		for (int i = 0; i < 15; i++) {
			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (5.0f, 10.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (3.0f, 10.0f), ForceMode2D.Impulse);
			}else{
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (3.0f, 10.0f), ForceMode2D.Impulse);
			}
		}
		yield return new WaitForSeconds (1);
	}

	IEnumerator
	Invincibility(){
		canTakeDamage = false;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.2f;
		yield return new WaitForSeconds (12);
		for (int i = 0; i < 10; i++){
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			yield return new WaitForSeconds (0.25f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			yield return new WaitForSeconds (0.25f);
		}
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		canTakeDamage = true;
	}

	IEnumerator
	CantGetHurt(){
		canTakeDamage = false;
		for (int i = 0; i < 10; i++){
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			yield return new WaitForSeconds (0.10f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			yield return new WaitForSeconds (0.10f);
		}
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		canTakeDamage = true;
	}

	IEnumerator GameOver (){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene("Main");
		yield return null;
	}
}