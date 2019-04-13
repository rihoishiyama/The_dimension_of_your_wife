using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextDot : MonoBehaviour 
{
	public string[] scenarios;
	[SerializeField] Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	public int currentLine = 0;
	private int lastUpdateCharacter = -1;

	public string playerName;

	[SerializeField]public GameObject lastButton;

	// [SerializeField] GameObject preStartImage;
	// [SerializeField] GameObject preStartButton;

	// [SerializeField] InputField nameInput;
	[SerializeField] public int aniInto1;

	// [SerializeField] public int aniInto2;

	// [SerializeField] public int aniInto3;

	// [SerializeField] public int aniInto4;

	// [SerializeField] public int aniInto5;

	// [SerializeField] public int aniInto6;

	// public GameObject chara;

	// [SerializeField] GameObject nameTextbox;

	// [SerializeField] GameObject nameButton;

	[SerializeField] AudioClip audioClip1;

	private AudioSource audioSource;


	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		// chara = GameObject.Find("airi");
		audioSource = gameObject.GetComponent<AudioSource>();
		playerName = PlayerPrefs.GetString("PLAYER_NAME","君");
		scenarios[5] = "あいり「" + playerName + "くんならまたちょうせんしてくれるよね・・・。」";

		SetNextLine();
	}

	void Update () 
	{
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0)){
				audioSource.clip = audioClip1;
        		audioSource.Play ();
				SetNextLine();
			}
		}else{
		// 完了してないなら文字をすべて表示する
			if(Input.GetMouseButtonDown(0)){
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;

		
		}

//アニメーションを入れる場所を生成

		if(currentLine == aniInto1){
			lastButton.SetActive(true);
		}else{
			lastButton.SetActive(false);
		}
		// if(currentLine == aniInto1){
		// 	chara.GetComponent<Chara3DController>().setAnimation1();
		// }

		// if(currentLine == aniInto2){
		// 	chara.GetComponent<Chara3DController>().setAnimation1();
		// }

		// if(currentLine == aniInto3){
		// 	chara.GetComponent<Chara3DController>().setAnimation3();
		// }

		// if(currentLine == aniInto4){
		// 	chara.GetComponent<Chara3DController>().setAnimation2();
		// }

		// if(currentLine == aniInto5){
		// 	chara.GetComponent<Chara3DController>().setAnimation4();
		// }

		// if(currentLine == aniInto6){
		// 	chara.GetComponent<Chara3DController>().setAnimation5();
		// }

	}

	public void MovePlayScene(){
		SceneManager.LoadScene("Main");
	}


	void SetNextLine()
	{
		currentText = scenarios[currentLine];
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine ++;
		lastUpdateCharacter = -1;

	}
}