using System;
using System.Collections.Generic;
using UnityEngine;

//キャラクターコントローラ
public class MyCharaController : MonoBehaviour {

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
		animater.Play("BYE");
	}

	

	



	//状態定数
	// public enum State {
	// 	NONE,  //非表示
	// 	HELLO, //入室
	// 	IDLE,  //待機
	// 	BYE    //退室
	// }

	// //情報
	// State state = State.NONE; //状態
	// float waitTime = 0;       //待機時間

	// //フレーム毎に呼ばれる
	// void Update () {
	// 	Vector3? touchPosition = GetTouchPosition ();
	// 	this.waitTime += Time.deltaTime;

	// 	//入室
	// 	if (this.state == State.HELLO) {
	// 		if (this.waitTime > 2) {
	// 			SetState (State.IDLE);
	// 		}
	// 	}
	// 	//待機
	// 	else if (this.state == State.IDLE) {
	// 		if (touchPosition != null && HitTest ((Vector3)touchPosition)) {
	// 			SetState (State.DANCE);
	// 		}
	// 	}
	// 	//ダンス
	// 	else if (this.state == State.DANCE) {
	// 		if (touchPosition != null && HitTest ((Vector3)touchPosition)) {
	// 			SetState (State.BYE);
	// 		}
	// 	}
	// 	//退室
	// 	else if (this.state == State.BYE) {
	// 		if (this.waitTime > 2) {
	// 			SetState (State.NONE);
	// 		}
	// 	}
	// }

	// //タッチ位置の取得
	// Vector3? GetTouchPosition() {
	// 	//マウス位置の取得(パソコン用)
	// 	if (Input.GetMouseButtonUp (0)) {
	// 		return Input.mousePosition;
	// 	}

	// 	//タッチ位置の取得
	// 	if (Input.touchCount > 0) {
	// 		Touch touch = Input.GetTouch (0);
	// 		if (touch.phase == TouchPhase.Ended) {
	// 			return touch.position;
	// 		}
	// 	}

	// 	//タッチなし
	// 	return null;
	// }

	// //ヒットテスト
	// bool HitTest(Vector3 touchPosition) {
	// 	if (touchPosition != null) {
	// 		Ray ray = Camera.main.ScreenPointToRay (touchPosition);
	// 		RaycastHit hit = new RaycastHit ();
	// 		if (Physics.Raycast (ray, out hit) &&
	// 			hit.collider.gameObject == this.gameObject) {
	// 			return true;
	// 		}
	// 	}
	// 	return false;
	// }

	// //状態の指定
	// public void SetState(State state) {
	// 	if (this.state == state) return;
	// 	this.state = state;
	// 	this.waitTime = 0;

	// 	//参照
	// 	Animator animater = GetComponent<Animator> ();

	// 	//非表示
	// 	if (state == State.NONE) {
	// 		gameObject.SetActive (false);
	// 	}
	// 	//入室
	// 	else if (state == State.HELLO) {
	// 		animater.CrossFade("hello",0);
	// 	} 
	// 	//待機
	// 	else if (state == State.IDLE) {
	// 		animater.CrossFade("idle",0);
	// 	} 
	// 	//ダンス
	// 	else if (state == State.DANCE) {
	// 		animater.CrossFade("dance",0);
	// 	} 
	// 	//退室
	// 	else if (state == State.BYE) {
	// 		animater.CrossFade("bye",0);
	// 	}
	// }
}