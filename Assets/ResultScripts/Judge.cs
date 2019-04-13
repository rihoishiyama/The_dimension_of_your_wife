using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {

	// Use this for initialization
	private string playerName;
	void Start () {
		if(!PlayerPrefs.HasKey("PLAYER_NAME")){
			SceneManager.LoadScene("FirstTitle");
		}else{
			SceneManager.LoadScene("Title");
		}
		playerName = PlayerPrefs.GetString("PLAYER_NAME","ベイビー");
		Debug.Log(playerName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
