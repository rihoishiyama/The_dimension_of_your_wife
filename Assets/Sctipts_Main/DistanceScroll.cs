using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class DistanceScroll : MonoBehaviour {

    [SerializeField] private GameObject distanceObj;
    [SerializeField] private Image distanceRectPos;
    public static int limitGoal = 60; //変更するもの　リザルトの秒数、壁生成のレベルデザイン
    private float distance = 265f;
    private float onescroll = 0f;

	// Use this for initialization
	void Start () 
    {

        distanceRectPos.rectTransform.anchoredPosition = new Vector3(-118, -1.25f, 0);
        onescroll = distance / limitGoal;

        PlayerController.breakWallNum.ObserveEveryValueChanged(_ => _.Value)
                        .Where(_ => _ >= 1)
                        .Subscribe(_ => ScrollPoint());

    }

    private void ScrollPoint()
    {
        Vector3 nowPos = distanceRectPos.rectTransform.anchoredPosition;
        nowPos.x += onescroll;
        if (!PlayerController.isFeverTouch)
        iTween.MoveTo(distanceObj, iTween.Hash("position", new Vector3(nowPos.x, -1.25f, 0),
                                               "time", 1f, 
                                               "islocal", true, 
                                               "delay", 0.2f));
        else if(PlayerController.isFeverTouch)
            iTween.MoveTo(distanceObj, iTween.Hash("position", new Vector3(nowPos.x, -1.25f, 0),
                               "time", 1f,
                               "islocal", true));

    }

}
