using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGenerator : MonoBehaviour 
{
    [SerializeField] private GameObject tutorialCanvas;

	void Awake () 
    {
        if (!PlayerPrefs.HasKey("In"))
        {
            SaveDataInitialize();
            tutorialCanvas.SetActive(true);
        }
    }

    private void SaveDataInitialize()
    {
        PlayerPrefs.SetInt("Init", 1); // ”Init”のキーをint型の値(1)で保存
        PlayerPrefs.Save();
    }
}
