using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour 
{
    [SerializeField] private GameObject[] tutorialPages = new GameObject[3];
    [SerializeField] private GameObject touchBtnObj, mainGametouchBtn, confirmBtnObj;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private Text airiComment;
    [SerializeField] private Sprite smile, angry, normal, happy;
    [SerializeField] private Image airiImg;
    [SerializeField] private AudioClip confirmClip;
    [SerializeField] private AudioSource bgm;

    private int pageNum;
    private string playerName;

    void Start () 
    {
        Button touchBtn = touchBtnObj.GetComponent<Button>();
        Button confirmBtn = confirmBtnObj.GetComponent<Button>();
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        playerName = PlayerPrefs.GetString("PLAYER_NAME", "君");

        InitTutorial();
        tutorialPages[pageNum].SetActive(true);
        AiriCommentText(playerName + "君、次元の壁をたくさん壊して私に会いにきて！" +
                        "壁の壊れやすくなっている部分を狙っていこう！▼", 3);

        touchBtn.onClick.AddListener(() => 
        {
            if (pageNum < 2) {
                audioSource.Play();
                tutorialPages[pageNum].SetActive(false);
                pageNum++;
                tutorialPages[pageNum].SetActive(true);

                if (pageNum == 1)
                    AiriCommentText("黄色いハートは３つ集めると一定時間どの部分の壁も壊せるようになるよ！" +
                        "タップした分だけスピードが上がるからどんどん連打しよう！▼", 4);
                else
                    AiriCommentText("壊しづらい青い壁に当たるとダメージを受けるよ。" +
                                    "３回ダメージを受けるとゲームオーバーになるから気をつけてね！", 2);
                
            } else {
                touchBtnObj.SetActive(false);
            }
        });

        confirmBtn.onClick.AddListener(() => 
        {
            audioSource.clip = confirmClip;
            audioSource.Play();
            bgm.Play();
            Invoke("CloseTutorial", 1);
        });
    }


    private void InitTutorial()
    {
        mainGametouchBtn.SetActive(false);

        foreach(GameObject go in tutorialPages)
        {
            go.SetActive(false);
        }
        pageNum = 0;
    }

    private void AiriCommentText(string cm, int faceNum)
    {
        switch (faceNum)
        {
            case 1:
                airiImg.sprite = smile;
                break;

            case 2:
                airiImg.sprite = angry;
                break;

            case 3:
                airiImg.sprite = normal;
                break;

            case 4:
                airiImg.sprite = happy;
                break;
        }

        airiComment.text = cm;
    }

    private void CloseTutorial()
    {
        mainGametouchBtn.SetActive(true);
        tutorialCanvas.SetActive(false);
    }

}
