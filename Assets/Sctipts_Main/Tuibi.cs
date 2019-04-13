using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuibi : MonoBehaviour
{

    private void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(2.74f, 5.75f, 0), "easeType", iTween.EaseType.easeOutQuad));
    }

}
