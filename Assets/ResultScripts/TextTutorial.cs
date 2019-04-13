using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTutorial : MonoBehaviour 
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

	[SerializeField] GameObject preStartButton;

	[SerializeField] GameObject hukidashi;

	[SerializeField] public AudioClip audioClip1;

	private AudioSource audioSource;

	// [SerializeField] InputField nameInput;

	// [SerializeField] public int aniInto;

	// [SerializeField] GameObject nameTextbox;

	// [SerializeField] GameObject nameButton;


	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		playerName = PlayerPrefs.GetString("PLAYER_NAME","君");
		scenarios[0] = "おはよう、" + playerName + "君";
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

		if(currentLine == 3){
			//uiText.SetActive(false);
			hukidashi.SetActive(false);
			preStartButton.SetActive(true);
		}
		}

//アニメーションを入れる場所を生成
		// if(currentLine == aniInto){
		// 	nameTextbox.SetActive(true);
		// 	nameButton.SetActive(true);
		// }else{
		// 	nameTextbox.SetActive(false);
		// 	nameButton.SetActive(false);
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