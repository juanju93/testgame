using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour {

	//use this in Player and put objets in place.

	public GameObject cam;
	public GameObject tokenUI;
	public GameObject token;
	public GameObject canvasOverlay;
	//public GameObject canvasOverlayText;
	public GameObject hearts;
	public static int health = 3;
	public static int tokens;
	public AudioClip collectToken;
	public AudioClip pain;
	private bool canTakeDamage = true;
	public AudioSource heartSound;
	public AudioSource invicibilitySound;
	private bool isInvin = false;


	void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.tag == "token") {
			GetComponent<AudioSource> ().PlayOneShot (collectToken, 0.5f);
			tokens++;
			//DataManagement.datamanagement.totalCollectedTokens++;
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
			Destroy (trig.gameObject);
			invicibilitySound.Play ();
			StartCoroutine (Invincibility ());
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
			if (health <= 0) {
				StartCoroutine (GameOver ());
			}else{
				StartCoroutine (CantGetHurt());
			}
		}
	}

	void TakeBigDamage(){
		if (canTakeDamage == true) {
			iTween.ShakePosition (cam, new Vector3 (0.4f, 0.4f, 0.4f), 1);   //iTween unity asset 
			GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f); //make different audio in future
			health = health - 3;
			StartCoroutine (GameOver ());
		}
	}

	void GainHeart(){
		if(health < 3){
			health++;
		}
	}

	public void ReloadScene (){
		SceneManager.LoadScene("Main");
	}

	IEnumerator
	SpawnExplosionOfTokens(){
		yield return new WaitForSeconds (0.3f);
		//put in some audio
		int explodingTokens = Random.Range(1,5);
		for (int i = 0; i < explodingTokens; i++) {
			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (5.0f, 15.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (10.0f, 15.0f), ForceMode2D.Impulse);
			}else{
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (10.0f, 15.0f), ForceMode2D.Impulse);
			}
		}
		yield return new WaitForSeconds (1);
	}

	IEnumerator
	SpawnExplosionOfTokensAfterBigToken(){
		//put some audio here
		for (int i = 0; i < 15; i++) {
			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (5.0f, 10.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (10.0f, 15.0f), ForceMode2D.Impulse);
			}else{
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (10.0f, 15.0f), ForceMode2D.Impulse);
			}
		}
		yield return new WaitForSeconds (1);
	}

	IEnumerator
	Invincibility(){
		canTakeDamage = false;
		isInvin = true;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.2f;
		//Time.timeScale = 1.25f;
		yield return new WaitForSeconds (12);
		for (int i = 0; i < 10; i++){
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			yield return new WaitForSeconds (0.25f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			yield return new WaitForSeconds (0.25f);
		}
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		//Time.timeScale = 1.0f;
		canTakeDamage = true;
		isInvin = false;
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

		if (isInvin == false) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
			canTakeDamage = true;
		}
	}

	IEnumerator GameOver (){
//		if (tokens > DataManagement.datamanagement.tokensHighScore) {
//			DataManagement.datamanagement.tokensHighScore = tokens;
//		}
//		DataManagement.datamanagement.SaveData ();
		Time.timeScale = 0.25f;
		canvasOverlay.SetActive (true);
		hearts.SetActive (false);
		//canvasOverlayText.GetComponent<Text> ().text = "Tokens Collected " + tokens.ToString ();
		gameObject.GetComponent<AudioSource> ().pitch = 0.8f;
		gameObject.GetComponent<Collider2D> ().enabled = false;
		gameObject.GetComponent<PlayerMove> ().enabled = false;
		yield return new WaitForSeconds (1);
	}
}