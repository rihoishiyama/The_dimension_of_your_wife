using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Transform targetPlayer;
    [SerializeField] GameObject cameraPos;
    [SerializeField] GameObject block;
    //private float y_pos = 1.0f;
    private float y_pos = 0f;


    private void Update()
    {
        //Debug.Log(targetPlayer.transform.position);
        if(PlayerController.isFeverTouch)
        {
            FeverCamera();
        }
        //FeverCamera();
    }

    public void StartTrailPlayer()
    {
        StartCoroutine(TrailPlayer());
    }

    private IEnumerator TrailPlayer()
    {
        //y_pos += 8;
        Debug.Log("壊した枚数（かめら） : " + PlayerController.breakWallNum.Value);

        y_pos = 1.0f + (PlayerController.breakWallNum.Value + 1) * 8.0f; 
        yield return new WaitForSeconds(0.2f);
        iTween.MoveTo(cameraPos, iTween.Hash(
            "position",
            new Vector3(0, y_pos, -10),
            "time", 0.5f, "easeType",
            iTween.EaseType.easeOutQuad,
            "oncomplete", "EndBlock",
            "oncompletetarget", this.gameObject
        ));
    }

    public void FeverCamera ()
    {
        Vector3 nowPos = cameraPos.transform.position;

        if (PlayerController.isGameOver || PlayerController.isGoal)
        {
            cameraPos.transform.position = nowPos;

        }
        else
        {
            cameraPos.transform.position = new Vector3(0, targetPlayer.transform.position.y + 3.5f, -10);
            nowPos = cameraPos.transform.position;

        }
    }

    private void EndBlock()
    {
        block.SetActive(false);
    }

}