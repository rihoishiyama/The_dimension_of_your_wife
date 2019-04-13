using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Text3D : MonoBehaviour 
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

	// [SerializeField] GameObject preStartImage;
	// [SerializeField] GameObject preStartButton;

	// [SerializeField] InputField nameInput;
	[SerializeField] public int aniInto1;

	[SerializeField] public int aniInto2;

	[SerializeField] public int aniInto3;

	[SerializeField] public int aniInto4;

	[SerializeField] public int aniInto5;

	[SerializeField] public int aniInto6;

	[SerializeField] public int lastPosition;

	[SerializeField] public GameObject lastButton;

    private GameObject chara;

	[SerializeField] public int audioInto1;

	[SerializeField] public int audioInto2;

	[SerializeField] public int audioInto3;

	[SerializeField] public AudioClip audioClip1;

	[SerializeField] public AudioClip audioClip2;

	[SerializeField] public AudioClip audioClip3;

	private AudioSource audioSource;

	private float clearTime;

	private float clearMinutes;

	private float clearSeconds;


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
		chara = GameObject.Find("airi");
		playerName = PlayerPrefs.GetString("PLAYER_NAME","君");
        clearTime = PlayerPrefs.GetFloat("ClearTime", 0);


        clearMinutes = (int)clearTime / 60;
		clearSeconds = clearTime - (clearMinutes * 60);

		scenarios[1] = "見てみてー！\n" + playerName + "君のおかげで3Dになれたよ！";
        scenarios[2] = clearMinutes + "分" + clearSeconds.ToString("F2") + "秒もかけて会いに来てくれて嬉しい！";

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

		// if(currentLine == aniInto){
		// 	nameTextbox.SetActive(true);
		// 	nameButton.SetActive(true);
		// }else{
		// 	nameTextbox.SetActive(false);
		// 	nameButton.SetActive(false);
		// }
        if(currentLine == aniInto1 || currentLine == aniInto2){
			chara.GetComponent<Chara3DController>().setAnimation1();
		}

		if(currentLine == aniInto3){
			chara.GetComponent<Chara3DController>().setAnimation3();
		}

		if(currentLine == aniInto4){
			chara.GetComponent<Chara3DController>().setAnimation2();
		}

		if(currentLine == aniInto5){
			chara.GetComponent<Chara3DController>().setAnimation4();
		}

		if(currentLine == aniInto6){
			chara.GetComponent<Chara3DController>().setAnimation5();
		}

		if(currentLine == lastPosition){
			lastButton.SetActive(true);
		}

		if(currentLine == audioInto2){
			audioSource.clip = audioClip2;
        	audioSource.Play ();
		}

		if(currentLine == audioInto3){
			audioSource.clip = audioClip3;
        	audioSource.Play ();
		}

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