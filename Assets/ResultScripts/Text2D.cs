using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Text2D : MonoBehaviour 
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

	[SerializeField] GameObject lastButton, tapBtnObj, skipBtnObj;

	[SerializeField] public int aniInto1;

	[SerializeField] public AudioClip audioClip1;

	private AudioSource audioSource;
    private Button tapBtn, skipBtn;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
        tapBtn = tapBtnObj.GetComponent<Button>();
        skipBtn = skipBtnObj.GetComponent<Button>();
		audioSource = gameObject.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Text2D")) 
            skipBtnObj.SetActive(true);
        else 
        {
            skipBtnObj.SetActive(false);
            SaveDataInitialize();
        }

        playerName = PlayerPrefs.GetString("PLAYER_NAME","君");
		scenarios[1] = "お疲れさま、" + playerName + "君。";

		SetNextLine();

        tapBtn.onClick.AddListener(() => 
        {
            // 文字の表示が完了してるならクリック時に次の行を表示する
            if (IsCompleteDisplayText && currentLine < scenarios.Length)
            {
                audioSource.clip = audioClip1;
                audioSource.Play();
                SetNextLine();
            } else {
                // 完了してないなら文字をすべて表示する
                timeUntilDisplay = 0;
            }
        });

        skipBtn.onClick.AddListener(() => SkipToLastLine());
    }

	void Update () 
	{
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

    public void SkipToLastLine()
    {
        currentText = scenarios[scenarios.Length - 1];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine = aniInto1;
        lastUpdateCharacter = -1;
        skipBtnObj.SetActive(false);
    }

    private void SaveDataInitialize()
    {
        PlayerPrefs.SetInt("Text2D", 1);
        PlayerPrefs.Save();
    }

}