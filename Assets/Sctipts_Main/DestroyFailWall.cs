using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyFailWall : MonoBehaviour {

    public Text text;
    [SerializeField] private WallBreak wallBreak;
    //private int breakNum = 0;

	void Update () {
        if (wallBreak.isDestroy){
            //if(breakNum <= 0 && PlayerController.isFeverTouch)
            //{
            //    breakNum++;
            //    //PlayerController.breakWallNum.Value++;
            //    //Debug.Log("WallBreakNum : " + PlayerController.breakWallNum.Value);
            //    //text.text = PlayerController.breakWallNum.Value.ToString();
            //}
            Destroy(this.gameObject, 3.0f);

        }
    }
}

