using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour {

	public GameObject prefab;
	private GameObject obj;

	// Use this for initialization
	void Start () {
		obj = Instantiate(prefab, new Vector3(0, 0, 80), Quaternion.identity);
		obj.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
