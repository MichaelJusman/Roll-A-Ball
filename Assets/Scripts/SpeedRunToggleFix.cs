using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRunToggleFix : MonoBehaviour
{
    GameController gameController;
    Toggle toggle;


    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        toggle = GetComponent<Toggle>();
        StartCoroutine(FixSpeedRunToggle());
    }

    // Update is called once per frame
    IEnumerator FixSpeedRunToggle()
    {
        yield return new WaitForEndOfFrame();
        if (gameController.gameType == GameType.SpeedRun)
            toggle.isOn = true;
        else
            toggle.isOn = false;

    }
}
