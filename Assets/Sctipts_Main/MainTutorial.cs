using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutorial : MonoBehaviour {
    [SerializeField] private GameObject tapText;

	// Use this for initialization
	void Start () {
        tapText.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.clickNum >= 1)
        {
            tapText.SetActive(false);
        }
    }

    public void ResetData()
    {
        tapText.SetActive(true);
    }
}
