using System;
using System.Collections.Generic;
using UnityEngine;

//キャラクターコントローラ
public class Chara3DController : MonoBehaviour {

	//Animator animater;

	void Start(){
		//animater = GetComponent<Animator>();
	}

	public void setAnimation1(){
		Animator animater = GetComponent<Animator>();
		animater.Play("HELLO");
	}

	public void setAnimation2(){
		Animator animater = GetComponent<Animator>();
		animater.Play("IDLE");
	}

	public void setAnimation3(){
		Animator animater = GetComponent<Animator>();
		animater.Play("SHOW");
	}

	public void setAnimation4(){
		Animator animater = GetComponent<Animator>();
		animater.Play("BORE");
	}

	public void setAnimation5(){
		Animator animater = GetComponent<Animator>();
		animater.Play("BYE");
	}
}

