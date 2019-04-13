using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    private int minute;
    private float seconds;
    public float saveSeconds;
    private float decimals;
    [SerializeField] private Text timerText;

    void Start()
    {
        SaveTimeData();
        minute = 0;
        seconds = 0f;
        saveSeconds = 0f;
        decimals = 0f;
    }

    void Update()
    {
        if(PlayerController.clickNum >= 1)
        {
            seconds += Time.deltaTime;
            saveSeconds += Time.deltaTime;
            decimals = (seconds - Mathf.Floor(seconds)) * 100;
            if (seconds >= 60f)
            {
                minute++;
                seconds = seconds - 60;
            }
            //　値が変わった時だけテキストUIを更新
            timerText.text = minute.ToString("00") + ":"
                + ((int)seconds).ToString("00") + ":"
                + ((int)decimals).ToString("00");

        }
    }

    public void LoadData()
    {

        //timeTextのtext属性にNOWのキーの文字列を代入。第二引数はデータが保存されてない場合に表示するもの
        timerText.text = PlayerPrefs.GetString("ClearTime", "");
    }




    public void SaveTimeData()
    {

        //NOWというキーでnowTimeの文字列をセーブ
        PlayerPrefs.SetFloat("ClearTime", saveSeconds);
        PlayerPrefs.Save();
    }

}
