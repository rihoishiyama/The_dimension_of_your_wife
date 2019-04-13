using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {
	InputField inputField;
	public string playerName;

	


	void Start () {
		InitInputField();
		inputField = GetComponent<InputField> ();
	}

	

	public void SaveName(){

		inputField = GameObject.Find("InputField").GetComponent<InputField> ();
		string playerName = inputField.text;
		PlayerPrefs.SetString("PLAYER_NAME",playerName);
		PlayerPrefs.Save();
		// PlayerPrefs.SetInt("JUDGE",1);
		// PlayerPrefs.Save();
		Debug.Log(playerName);
		SceneManager.LoadScene("Tutorial");
	}
	void InitInputField() {
 
        // 値をリセット
        inputField.text = "";
 
        // フォーカス
        inputField.ActivateInputField();
    }


	// Use this for initialization
	// [SerializeField] GameObject TextController;

	// public bool check_last = false;
	
	
	// Update is called once per frame
	//void Update () {

	// 	///if(Input.GetMouseButtonDown(0)){
	// 		///check_last = TextController.check_last;
	// 		///if(check_last == true){
	// 			///this.gameObject.SetActive(true);
	// 		///}else{
	// 			///this.gameObject.SetActive(false);
	// 		}
	// // public void delete(){
	// // 	this.gameObject.SetActive(false);
	// // }

	// // public void generate(){
	// // 	this.gameObject.SetActive(true);
	// // }

	// 	}
	
}
