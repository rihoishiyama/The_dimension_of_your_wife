using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIExtensions;
using UnityEngine.SceneManagement;

public class FlashHand : MonoBehaviour {

    [SerializeField] private TextTutorial textTutorial;
    private UIShiny uIShiny;
    private bool isStart = false;
    private float sumTime = 0;
    private float duration = 0.0f;
    private float maxBright = 0.8f;

    // Use this for initialization
    void Start () {
        isStart = false;
        uIShiny = GetComponent<UIShiny>();
        uIShiny.gloss = 0.2f;
        uIShiny.brightness = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (textTutorial.currentLine == 3 && !isStart)
        {
            sumTime += Time.deltaTime;
            uIShiny.brightness = Mathf.PingPong(sumTime, maxBright);
        }
    }

    public void TapHandButton()
    {
        isStart = true;
        StartCoroutine(_TapHandButton());
    }

    public IEnumerator _TapHandButton()
    {
        uIShiny.brightness = maxBright;
        uIShiny.gloss = 1f;

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }
}
