using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class MyHitTest : MonoBehaviour {
    
    public Camera camera;
    public GameObject character;
    public GameObject label;
    public GameObject generatePlane;
	public GameObject comment;
	public GameObject hukidashi;

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (label.activeSelf){
			int count = generatePlane.GetComponent<MyGeneratePlane> ().GetPlanesCount ();
			string text = count == 0 ?
				 "カメラで周りを見回してね！\n" :
				  "キャラクターを配置する場所を\nタップしてね！";
			label.GetComponent<Text>().text = text;

			Vector3? touchPosition = GetTouchPosition();
			if (touchPosition != null){
				//ヒットテスト
				HitTest ((Vector3)touchPosition);
			}
		} else {
			//キャラクター非表示時の処理
			if(!character.activeSelf){
				//ラベルと平面の表示
				this.label.SetActive(true);
				this.generatePlane.GetComponent<MyGeneratePlane>().SetPlaneVisible(true);
			}
		}
    }
    
    Vector3? GetTouchPosition(){
		#if UNITY_EDITOR
		if (Input.GetMouseButtonUp(0)){
			ShowCharacter(new Vector3 (0, 0, 1));
			return null;
		}
		#endif

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if(touch.phase == TouchPhase.Ended){
				return touch.position;
			}
		}
		return null;
	}
    
    void HitTest(Vector3 touchPosition){
        ARHitTestResultType[] resultTypes = {
            ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent,
            ARHitTestResultType.ARHitTestResultTypeHorizontalPlane
        };
        
        Vector3 viewportPoint = camera.ScreenToViewportPoint(touchPosition);
        ARPoint arPoint = new ARPoint{
            x = viewportPoint.x,
            y = viewportPoint.y
        };
        
        foreach (ARHitTestResultType resultType in resultTypes){
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.
                GetARSessionNativeInterface().HitTest(arPoint, resultType);
            foreach(var hitResult in hitResults){
                ShowCharacter(UnityARMatrixOps.GetPosition(hitResult.worldTransform));
                return;
            }
        }
        
    }
    void ShowCharacter(Vector3 position) {
		this.character.transform.position = position;

		Vector3 cameraPos = new Vector3(
			camera.transform.position.x,
			character.transform.position.y,
			camera.transform.position.z);
		character.transform.rotation =  Quaternion.LookRotation(
			cameraPos-character.transform.position);

		this.character.SetActive(true);

		this.label.SetActive (false);
		this.generatePlane.GetComponent<MyGeneratePlane>().SetPlaneVisible(false);
		
		this.hukidashi.SetActive(true);
		this.comment.SetActive(true);
		
		//this.comment.GetComponent<Text>().SetActive(true);
	}
}
/*using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
//ヒットテスト
public class MyHitTest : MonoBehaviour {
	//参照
	public Camera camera;            //カメラ
	public GameObject character;     //キャラクター
	public GameObject label;         //ラベル
	public GameObject generatePlane; //平面生成

	//フレーム毎に呼ばれる
	void Update () {
		if (label.activeSelf) {
			//ラベルの更新
			int count = generatePlane.GetComponent<MyGeneratePlane> ().GetPlanesCount ();
			string text = count == 0 ?
				"カメラで周囲を見回して\n平面を探してください" :
				"キャラクターを配置する場所を\nタップしてください";
			label.GetComponent<Text> ().text = text;

			//タッチ時の処理
			Vector3? touchPosition = GetTouchPosition ();
			if (touchPosition != null) {
				//ヒットテスト
				HitTest ((Vector3)touchPosition);
			}
		} else {
			//キャラクター非表示時の処理
			if (!character.activeSelf) {
				//ラベルと平面の表示
				this.label.SetActive (true);
				this.generatePlane.GetComponent<MyGeneratePlane> ().SetPlaneVisible (true);
			}
		}
	}

	//タッチ位置の取得
	Vector3? GetTouchPosition() {
		//マウス位置の取得(Unityエディタ用)
		#if UNITY_EDITOR
		if (Input.GetMouseButtonUp (0)) {
			ShowCharacter (new Vector3 (0, 0, 1));
			return null;
		}
		#endif

		//タッチ位置の取得
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Ended) {
				return touch.position;
			}
		}
		return null;
	}

	//ヒットテスト
	void HitTest (Vector3 touchPosition)    {
		//ヒットテストの結果種別の指定
		ARHitTestResultType[] resultTypes = {
			ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
			ARHitTestResultType.ARHitTestResultTypeEstimatedHorizontalPlane
		}; 

		//ARPointの生成
		Vector3 viewportPoint = camera.ScreenToViewportPoint (touchPosition);
		ARPoint arPoint = new ARPoint {
			x = viewportPoint.x,
			y = viewportPoint.y
		};

		//ヒットテストの実行
		foreach (ARHitTestResultType resultType in resultTypes) {
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.
				GetARSessionNativeInterface ().HitTest(arPoint, resultType);
			foreach (var hitResult in hitResults) {
				//キャラクターの表示
				ShowCharacter (UnityARMatrixOps.GetPosition (hitResult.worldTransform));
				return;
			}
		}            
	}

	//キャラクターの表示
	void ShowCharacter(Vector3 position) {
		//キャラクターの位置と向きの指定
		this.character.transform.position = position;

		//キャラクターの向きをカメラ方向に指定
		Vector3 cameraPos = new Vector3 (
			camera.transform.position.x,
			character.transform.position.y,
			camera.transform.position.z);
		character.transform.rotation =  Quaternion.LookRotation(
			cameraPos-character.transform.position);

		//キャラクターの表示
		this.character.SetActive (true);
		this.character.GetComponent<MyCharacterController> ().SetState (
			MyCharacterController.State.HELLO);

		//ラベルと平面の非表示
		this.label.SetActive (false);
		this.generatePlane.GetComponent<MyGeneratePlane> ().SetPlaneVisible (false);
	}
}*/