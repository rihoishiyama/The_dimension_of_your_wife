using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	string direction;


	void Flick(){
 	if (Input.GetKeyDown(KeyCode.Mouse0)){
     	 touchStartPos = new Vector3(Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  Input.mousePosition.z);
    }

 	if (Input.GetKeyUp(KeyCode.Mouse0)){
     		touchEndPos = new Vector3(Input.mousePosition.x,
                           		Input.mousePosition.y,
                            	Input.mousePosition.z);
     GetDirection();
    }
}

public void GetDirection(){
     float directionX = touchEndPos.x - touchStartPos.x;
     float directionY = touchEndPos.y - touchStartPos.y;

     if (Mathf.Abs(directionY) < Mathf.Abs(directionX)){
       if (30 < directionX){
                //右向きにフリック
                direction = "right";
           }else if (-30 > directionX){
                //左向きにフリック
                direction = "left";
            }
        }
    else if (Mathf.Abs(directionX)<Mathf.Abs(directionY)){
            if (30 < directionY){
                //上向きにフリック
                direction = "up";
            }else if (-30 > directionY){
                //下向きのフリック
                direction = "down";
            }
    }else{
                //タッチを検出
                direction = "touch";
          }
}
void Update()
{
	switch(direction){
     case "up":
           //上フリックされた時の処理
		  
      　　 break;

     case "down":
           //下フリックされた時の処理
    　　　 break;

     case "right":
　　　　　　//右フリックされた時の処理
     　　　break;

     case "left":
　　　　　　//左フリックされた時の処理
     　　　break;

     case "touch":
　　　　　 //タッチされた時の処理
          break;
    }
}

	
	
}
