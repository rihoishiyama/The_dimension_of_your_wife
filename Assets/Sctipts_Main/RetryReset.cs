using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryReset : MonoBehaviour 
{
    [SerializeField] TimeController timeController;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;
    [SerializeField] GenerateWall generateWall;
    [SerializeField] MainTutorial mainTutorial;
    [SerializeField] PauseButton pauseButton;

    public void RestData()
    {
        iTween.Stop();
        timeController.ResetData();
        playerController.ResetData();
        cameraController.ResetPos();
        generateWall.ResetPos();
        mainTutorial.ResetData();
        pauseButton.ResetData();
    }
}
