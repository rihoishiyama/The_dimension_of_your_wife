using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalTitle : MonoBehaviour 
{
    private Button startBtn;

	void Awake () 
    {
        if(PlayerPrefs.HasKey("Block"))
        {
            startBtn = this.gameObject.GetComponent<Button>();
            startBtn.interactable = false;
            StartCoroutine(BlockTap());
        }
    }

    private IEnumerator BlockTap()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.DeleteKey("Block");
        startBtn.interactable = true;
    }

}
