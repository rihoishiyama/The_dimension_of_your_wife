using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// [SerializeField] public AudioClip audioClip1;

	// //private AudioClip audioclip;

	// private AudioSource audioSource;

	// Use this for initialization

	public void GameStart(){

		// audioSource = gameObject.GetComponent<AudioSource>();
		// audioSource.clip = audioClip1;
        // audioSource.Play ();

		SceneManager.LoadScene("Main");
	}

	// public void AlbumStart(){
	// 	SceneManager.LoadScene("Album");
	// }

	// public void HelpScene(){
	// 	SceneManager.LoadScene("Help");
	// }

	public void NameSetStart(){
		//audioclip = audioClip1;
		// audioSource = gameObject.GetComponent<AudioSource>();
		// audioSource.clip = audioClip1;
        // audioSource.Play ();
		SceneManager.LoadScene("NameSet");
	}
	
	public void GoTitle(){

        // audioSource = gameObject.GetComponent<AudioSource>();
        // audioSource.clip = audioClip1;
        // audioSource.Play ();
        PlayerPrefs.SetInt("Block", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Title");
	}

	public void GoFirstTitle(){
		
		// audioSource = gameObject.GetComponent<AudioSource>();
		// audioSource.clip = audioClip1;
        // audioSource.Play ();
		SceneManager.LoadScene("FirstTitle");
	}

	public void DeletePlayerName(){
		PlayerPrefs.DeleteAll();
		Debug.Log("データを削除しました。");
	}
}
