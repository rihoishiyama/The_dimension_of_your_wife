using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWall : MonoBehaviour {

    public float interval;
    private Vector3 bufferPos, firstWallPos;
    private float bufferScale;
    private int generateNum = 1; //firstwallも含むため
    private float randomScale = 0f;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject kakera;
    [SerializeField] private Transform firstWall;
    [SerializeField] private GameObject parentObj;

	void Start () 
    {
        firstWallPos = firstWall.position;
        bufferPos = firstWallPos;
        InstantiateWall();
        InstantiateWall();
    }

    public void InstantiateWall()
    {
        if(generateNum < DistanceScroll.limitGoal)
        {
            generateNum++;
            Vector3 temp = bufferPos;
            temp.x = Random.Range(-1.69f, 2.1f);
            temp.y += interval;
            bufferPos = temp;

            //壊した数によって幅を決める
            if (PlayerController.breakWallNum.Value < 13)
                randomScale = Random.Range(1.1f, 1.45f);
            else if(PlayerController.breakWallNum.Value < 35)
                randomScale = Random.Range(0.7f, 1.1f);
            else
                randomScale = Random.Range(0.59f, 0.9f);


            GameObject obj = (GameObject)Instantiate(Wall, bufferPos, Quaternion.identity);
            obj.transform.localScale = new Vector2(randomScale , 1f);
            bufferScale = obj.transform.localScale.x;
            obj.transform.parent = parentObj.transform;
            obj.SetActive(true);

            if (!PlayerController.isFeverTouch)
            {
                GameObject kakeraObj;
                int randomGenerate = 0;
                randomGenerate = Random.Range(0,2);
                //Debug.Log(randomGenerate);
                //カケラを生成
                //if(randomGenerate < 1 && randomGenerate >= 0){
                kakeraObj = (GameObject)Instantiate(kakera, new Vector3(Random.Range(-0.1f * bufferScale, 0.95f * bufferScale) + bufferPos.x, temp.y + 3, 0), Quaternion.identity);
                kakeraObj.transform.parent = parentObj.transform;
                kakeraObj.SetActive(true);

                //}
            }

        }
        Debug.Log("generateNum : " + generateNum);
    }

    public void ResetPos()
    {
        foreach (Transform child in parentObj.transform)
        {
            Destroy(child.gameObject);
        }
        generateNum = 1;
        bufferPos = firstWallPos;
        GameObject go = Instantiate(Wall, bufferPos, Quaternion.identity);
        go.transform.parent = parentObj.transform;
        go.SetActive(true);
        InstantiateWall();
        InstantiateWall();
    }
}
