using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
    [Header("プレイヤーの横移動のスピード")]
    public float playerSpeed = 1.0f;
    public float m_speed;
    public static bool isFeverTouch = false;

    [SerializeField] TimeController timeController;
    [SerializeField] GameObject wall;
    [SerializeField] CameraController cameraController;
    [SerializeField] GenerateWall generateWall;
    [SerializeField] GameObject block, compBlock;
    [SerializeField] Button click;
    [SerializeField] GameObject tapObj;
    [SerializeField] SpriteRenderer heart1, heart2, heart3;
    [SerializeField] Sprite getKakera, NotKakera;
    [SerializeField] GameObject gameClearText, gameOverEffect, childPlayer;
    [SerializeField] Text airiComment;
    [SerializeField] Image airiRenderer;
    [SerializeField] Sprite smile, angry, normal, happy, zeroPenalty, onePenalty, twoPenalty;

    private SpriteRenderer childPlayersr;

    private Animator anim, clearAnima, gameOverAnim, childplayerTutorialAnim;
    private float count = 0;
    private float bufferTime = 0.0f;
    private int failedNum = 0;
    public static int clickNum = 0;
    public static bool isGameOver = false, isGoal = false;

    public bool isCountdownStart = false, isAnima = false;

    public static ReactiveProperty<int> kakeraCount = new ReactiveProperty<int>(0);
    public static ReactiveProperty<int> breakWallNum = new ReactiveProperty<int>(0);

    private float wallY;
    public int brokeWallId = 0;

    public AudioClip breakWallSound;
    public AudioClip goStraightSound;
    public AudioClip FailWallSound;
    public AudioClip startFeverSound;
    private AudioSource[] sources;

    void Start () 
    {
        sources = gameObject.GetComponents<AudioSource>();
        compBlock.SetActive(false);

        anim = this.gameObject.GetComponent<Animator>();
        clearAnima = gameClearText.GetComponent<Animator>();
        gameOverAnim = gameOverEffect.GetComponent<Animator>();
        childplayerTutorialAnim = childPlayer.GetComponent<Animator>();

        childPlayersr = childPlayer.GetComponent<SpriteRenderer>();
        childPlayersr.sprite = zeroPenalty;

        AiriCommentText("画面をタップでスタートだよ！", 3);

        //※１＿カケラが３つ取れたらフィーバータイム
        kakeraCount.ObserveEveryValueChanged(_ => _.Value)
                   .Where(_ => _ >= 3)
                   .Subscribe(_ => StartFeverCount());

        kakeraCount.ObserveEveryValueChanged(_ => _.Value)
                   .Where(_ => _ < 2 && _ > 0)
                   .Subscribe(_ => AiriCommentText("ハートを集めてフィーバーだ！", 4));

        breakWallNum.ObserveEveryValueChanged(_ => _.Value)
                   .Where(_ => _ >= 3)
                   .Subscribe(_ => generateWall.InstantiateWall());

    }

    void Update() 
    {
        float curY = transform.position.y;
        int wallIndex = brokeWallId + 1;
        //Debug.Log("brokeWallId : " + brokeWallId);
        //※１が発動したらプレイヤーの動き変化
        if (isFeverTouch && !PauseButton.isPause)
        {
            FeverTime();
            curY = transform.position.y;
            wallIndex = brokeWallId + 1;
            while (true)
            {
                wallY = 0.819f + (wallIndex - 1) * 8;
                if (curY > wallY)
                {
                    wallIndex++;
                    brokeWallId++;
                    breakWallNum.Value = brokeWallId;
                    Debug.Log("brokeWallId : " + brokeWallId);
                    if (breakWallNum.Value >= DistanceScroll.limitGoal)
                    {
                        ActiveCompBlock();
                        StartCoroutine(GoalAction());
                    }
                }
                else
                {
                    break;
                }
            }
        }


        else if (clickNum >= 1 && !isGoal)
        {
            if(clickNum == 1){
                childplayerTutorialAnim.SetTrigger("GameStart");
            }

            HorizontalAutoScroll();
            curY = transform.position.y;
            wallIndex = brokeWallId + 1;
            while (true)
            {
                wallY = 1.16f + (wallIndex - 1) * 8;
                if (curY >= wallY)
                {
                    wallIndex++;
                    brokeWallId++;
                    breakWallNum.Value = brokeWallId;
                    Debug.Log("brokeWallId : " + brokeWallId);
                    if (breakWallNum.Value >= DistanceScroll.limitGoal)
                    {
                        StartCoroutine(GoalAction());
                    }
                }
                else
                {
                    break;
                }
            }

        }

        //ここから
        if (Input.GetMouseButton(0))
        {
            isCountdownStart = true;
            count = 0.0f;
        }

        if (isCountdownStart)
        {
            count += Time.deltaTime;
        }

        if (count >= 0.5f)
        {
            count = 0;
            m_speed = 2.0f;
            isCountdownStart = false;
        }
        //ここまでが連続タップでスピード上がって最新のタップから0.5秒たったら元のスピードに戻る
    }


    //横移動の計算
    private void HorizontalAutoScroll()
    {
        if (!isAnima)
        {
            if (breakWallNum.Value < 13)
                playerSpeed = 2.0f;
            else if (PlayerController.breakWallNum.Value < 35)
                playerSpeed = 3.0f;
            else
                playerSpeed = 4.0f;

            Vector3 temp = transform.position;
            bufferTime += Time.deltaTime;
            temp.x = Mathf.Sin(bufferTime * playerSpeed) * 2.3f;
            transform.position = temp;
        }
    }

    //タップされたら rayがあたり壁に当たった時だけアニメーションする
    public void GoStraight()
    {
        if (!isFeverTouch)
        {
            AiriCommentText("ゴールまでがんばって！応援してるよ！", 1);
            sources[0].clip = goStraightSound;
            sources[0].Play();
            clickNum++;
            isAnima = true;
            ActiveBlock();
            Vector3 rayStartPos = transform.position;
            rayStartPos.y += 1.5f;

            Ray ray = new Ray(rayStartPos, transform.forward);

            RaycastHit2D hit = Physics2D.BoxCast(rayStartPos, new Vector2(0.29f, 0.5f), 0, Vector2.up);

            if (hit.collider)
            {
                if (hit.collider.tag == "SuccessWall")
                {
                    StartCoroutine(PlayBreakWall());
                    anim.SetTrigger("Straight");
                    cameraController.StartTrailPlayer();
                }
                else
                {
                    sources[1].clip = FailWallSound;
                    sources[1].Play();

                    failedNum++;
                    if (failedNum == 1)
                    {
                        AiriCommentText("ダメージ受けてるよ！気をつけて！", 2);
                        childPlayersr.sprite = onePenalty;
                    }

                    else if(failedNum == 2)
                    {
                        AiriCommentText("ダメージ受けてるよ！気をつけて！", 2);
                        childPlayersr.sprite = twoPenalty;
                    }

                    if (failedNum >= 3)
                    {
                        isGameOver = true;
                        isGoal = false;
                        clickNum = 0;//時間止まる
                        anim.SetTrigger("isGameOver");
                        SaveBreakNumData();

                        OnDataInitialized();

                        gameOverAnim.SetTrigger("isGameOver");
                        StartCoroutine(DivideGameOverResult());

                    }
                    else
                    {
                        anim.SetTrigger("FailWall");
                    }
                }
            }
        }
    }

    IEnumerator PlayBreakWall()
    {
        yield return new WaitForSeconds(0.3f);

        sources[1].clip = breakWallSound;
        sources[1].Play();
    }

    IEnumerator DivideGameOverResult()
    {
        yield return new WaitForSeconds(2.0f);

        if (brokeWallId < (DistanceScroll.limitGoal / 3)/* * 2 */)
            SceneManager.LoadScene("ResultDot");
        else
            SceneManager.LoadScene("Result2D");

    }

    //フィーバー時の移動
    public void FeverTime()
    {
        var velocity = new Vector3(0, 0.1f, 0) * m_speed;
        transform.localPosition += velocity;
    }

    //フィーバー時にタップするとスピードあがるよ
    public void SpeedUp()
    {
        if (isFeverTouch)
        {
            m_speed += 1.0f;
        }
    }

    private void StartFeverCount()
    {
        var clones = GameObject.FindGameObjectsWithTag("Kakera");
        foreach (var clone in clones)
        {
           Destroy(clone);
        }

        StartCoroutine(FeverCountDown());
    }

    IEnumerator FeverCountDown()
    {
        sources[1].clip = startFeverSound;
        sources[1].Play();

        isFeverTouch = true;
        tapObj.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        m_speed = 0;
        ActiveBlock();

        yield return new WaitForSeconds(0.1f);

        float diff = -2.5f + breakWallNum.Value * 8.0f;
        iTween.MoveTo(
            this.gameObject, 
            iTween.Hash(
                "position",
                new Vector3(0, diff, 0),
                "time", 1f, 
                "oncomplete", "EndFever",
                "oncompletetarget", this.gameObject
        ));
        //Debug.Log("壊した枚数 : "+ breakWallNum.Value);
    }

    public void EndFever()
    {
        kakeraCount.Value = 0;
        heart1.sprite = NotKakera;
        heart2.sprite = NotKakera;
        heart3.sprite = NotKakera;
        tapObj.SetActive(false);
        block.SetActive(false);
        isFeverTouch = false;
        isAnima = false;
    }


    IEnumerator GoalAction()
    {
        isGoal = true;
        clickNum = 0;//時間止まる
        clearAnima.SetTrigger("isClear");
        anim.SetTrigger("isGoal");

        while (isFeverTouch)
            yield return null;

        yield return new WaitForSeconds(2.0f);
        timeController.SaveTimeData();

        OnDataInitialized();

        if(timeController.saveSeconds < 55.0f)
            SceneManager.LoadScene("ResultAR");
        else
            SceneManager.LoadScene("Result3D");
    }

    public void ActiveCompBlock()
    {
        compBlock.SetActive(true);
    }

    public void ActiveBlock()
    {
        block.SetActive(true);
    }

    public void InactiveBlock()
    {
        block.SetActive(false);
        isAnima = false;
    }

    public void SaveBreakNumData()
    {

        PlayerPrefs.SetFloat("BreakNum", breakWallNum.Value);
        PlayerPrefs.Save();
    }

    //初期化
    private void OnDataInitialized()
    {
        breakWallNum.Value = 0;
        kakeraCount.Value = 0;
        isGameOver = false;
        isGoal = false;
        isFeverTouch = false;
    }


    //あいりちゃんの顔と表情を入れる
    public void AiriCommentText(string cm, int faceNum)
    {
        switch(faceNum)
        {
            case 1:
                airiRenderer.sprite = smile;
                break;

            case 2:
                airiRenderer.sprite = angry;
                break;

            case 3:
                airiRenderer.sprite = normal;
                break;

            case 4:
                airiRenderer.sprite = happy;
                break;
        }

        airiComment.text = cm;
    }

    public void ResetData()
    {
        breakWallNum.Value = 0;
        kakeraCount.Value = 0;
        isGameOver = false;
        isGoal = false;
        isFeverTouch = false;
        clickNum = 0;
        brokeWallId = 0;
        failedNum = 0;
        bufferTime = 0;
        childPlayersr.sprite = zeroPenalty;

        // PlayerのPositionを初期化
        Vector3 temp = transform.position;
        temp.x = 0;
        temp.y = -2.5f;
        transform.position = temp;


        AiriCommentText("画面をタップでスタートだよ！", 3);
        EndFever();
    }

}