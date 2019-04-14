using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGenerator : MonoBehaviour 
{
    [SerializeField] private GameObject tutorialCanvas;

	void Awake () 
    {
        if (!PlayerPrefs.HasKey("Init"))
        {
            SaveDataInitialize();
            tutorialCanvas.SetActive(true);
        }
    }

    private void SaveDataInitialize()
    {
        PlayerPrefs.SetInt("Init", 1); 
        PlayerPrefs.Save();
    }
}
