using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class KakeraController : MonoBehaviour {

    //[SerializeField] private Image heart1, heart2, heart3;
    [SerializeField] private SpriteRenderer heart1, heart2, heart3;
    [SerializeField] private Sprite getKakera, NotKakera;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject mainCam, heartObj;

    private AudioSource audioSource;
    private BoxCollider2D bc;

    private float[] adjustedValue_posY = { 2.0f, 2.2f, 2.4f };
    private float[] adjustedValue_posX = { -1.25f, -1.35f, -1.45f };

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();
        if (other.tag == "Player")
        {
            bc.enabled = false;
            PlayerController.kakeraCount.Value++;

            //Destroy(this.gameObject);

            KakeraCountAction();
            //Debug.Log("kakeraNum : " + PlayerController.kakeraCount.Value);
        }
    }

    void KakeraCountAction()
    {
        if (PlayerController.kakeraCount.Value % 3 == 1)
        {
            float pos_y = 1.0f + (PlayerController.breakWallNum.Value) * 8.0f + 2.01f +adjustedValue_posY[0];
            Debug.Log("kakeraNum1 : " + pos_y);
            float pos_x = adjustedValue_posX[0] + heartObj.transform.position.x;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(pos_x, pos_y, 0),
                                                       "easeType", iTween.EaseType.easeOutQuad,
                                                       "time", 0.5f,
                                                       "oncomplete", "DestroyKakeraObj",
                                                       "oncompletetarget", this.gameObject
                                                      ));
        }
        else if (PlayerController.kakeraCount.Value % 3 == 2)
        {
            float pos_y = 1.0f + (PlayerController.breakWallNum.Value) * 8.0f + 2.18f + adjustedValue_posY[1];
            Debug.Log("kakeraNum2 : " + pos_y);

            float pos_x = adjustedValue_posX[1] + heartObj.transform.position.x;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(pos_x, pos_y, 0),
                                                       "easeType", iTween.EaseType.easeOutQuad,
                                                       "time", 0.5f,
                                                       "oncomplete", "DestroyKakeraObj",
                                                       "oncompletetarget", this.gameObject
                                                      ));
        }
        else if (PlayerController.kakeraCount.Value % 3 == 0)
        {
            float pos_y = 1.0f + (PlayerController.breakWallNum.Value) * 8.0f + 2.37f + adjustedValue_posY[2];
            Debug.Log("kakeraNum3 : " + pos_y);

            float pos_x = adjustedValue_posX[2] + heartObj.transform.position.x;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(pos_x, pos_y, 0),
                                                       "easeType", iTween.EaseType.easeOutQuad,
                                                       "time", 0.1f,
                                                       "oncomplete", "DestroyKakeraObj",
                                                       "oncompletetarget", this.gameObject
                                                      ));
            heart3.sprite = getKakera;
        }

    }

    void DestroyKakeraObj()
    {
        if (PlayerController.kakeraCount.Value % 3 == 1)
            heart1.sprite = getKakera;
        else if (PlayerController.kakeraCount.Value % 3 == 2)
            heart2.sprite = getKakera;
        //else if (PlayerController.kakeraCount.Value % 3 == 0)
            //heart3.sprite = getKakera;

        Destroy(this.gameObject);
    }

}
