using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAction : MonoBehaviour {

    [SerializeField] private Image distanceRectPos;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

    }

    public void RetryButton()
    {
        //audioSource.Play();

        Time.timeScale = 1;
        distanceRectPos.rectTransform.anchoredPosition = new Vector3(-118, -1.25f, 0);
        PlayerController.breakWallNum.Value = 0;
        PlayerController.kakeraCount.Value = 0;
        PlayerController.isGameOver = false;
        PlayerController.isGoal = false;
        PlayerController.isFeverTouch = false;
        PlayerController.clickNum = 0;
        PauseButton.isPause = false;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void TitleButton()
    {
        //audioSource.Play();
        RetryButton();
        SceneManager.LoadScene("Title");
    }
}
