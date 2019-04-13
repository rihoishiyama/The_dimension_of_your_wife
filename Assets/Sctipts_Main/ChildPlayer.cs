using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlayer : MonoBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = this.gameObject.GetComponent<Animator>();
    }

    public void PlayPlayerScaleAnim()
    {
        if(!PlayerController.isFeverTouch)
        anim.SetTrigger("Straight");
    }
	
}
