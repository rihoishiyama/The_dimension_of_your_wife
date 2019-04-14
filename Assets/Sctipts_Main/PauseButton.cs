using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class PauseButton : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Sprite playBackGraphic;
    [SerializeField] Sprite pauseGraphic;
    [SerializeField] GameObject PauseObj;
    [SerializeField] Toggle Toggle;
    private AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    public static bool isPause;

    void Start()
    {
        isPause = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

        GetComponent<Toggle>().onValueChanged.AddListener((value) => {
            OnValueChanged(value);
        });
    }

    void OnValueChanged(bool value)
    {

        if (Toggle.isOn)
        {
            audioSource.Play();
            image.sprite = playBackGraphic;
            PauseObj.SetActive(true);
            isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            audioSource.Play();
            image.sprite = pauseGraphic;
            PauseObj.SetActive(false);
            isPause = false;
            Time.timeScale = 1;
        }
    }

    public void ResetData()
    {
        Toggle.isOn = false;
        PauseObj.SetActive(false);
        image.sprite = pauseGraphic;
    }

}
