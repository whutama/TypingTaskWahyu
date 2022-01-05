using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScriptV2 : MonoBehaviour
{
    private KeyboardControl keyboardController;
    private GameObject button;

    private void Start()
    {
        keyboardController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyboardControl>();
        button = this.transform.GetChild(1).gameObject;
        //hide keyboard at start
    }
    public void Resume()
    {
        button.SetActive(false);
        KeyboardControl.TypingEnabled = true;
        keyboardController.ResumeTimer();
    }
    public void Show()
    {
        button.SetActive(true);
    }
}
