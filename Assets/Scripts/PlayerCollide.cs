using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour {

	public GameObject cam;
	public int tokens;
	public GameObject tokenUI;
	public static int health = 3;
	public AudioClip collectToken;
	public AudioClip pain;

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
			tokens = tokens + 10;
			tokenUI.GetComponent<Text> ().text = (tokens.ToString ());
			Destroy (trig.gameObject);
		}
		if (trig.gameObject.tag == "log"){
			TakeDamage ();
		}
		if (trig.gameObject.tag == "biglog") {
			TakeBigDamage ();
		}
	}

	void TakeDamage(){
		iTween.ShakePosition (cam, new Vector3 (0.2f, 0.2f, 0.2f), 1);   //iTween unity asset 
		GetComponent<AudioSource>().PlayOneShot (pain, 0.5f);
		health--;
	}

	void TakeBigDamage(){
		iTween.ShakePosition (cam, new Vector3 (0.4f, 0.4f, 0.4f), 1);   //iTween unity asset 
		GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f); //make different audio
		health = health -3;
	}

	IEnumerator GameOver (){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene("Main");
		yield return null;
	}
}