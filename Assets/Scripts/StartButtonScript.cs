using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    private KeyboardControl keyboardController;

    private void Start()
    {
        keyboardController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyboardControl>();
        //hide keyboard at start
    }
    public void Resume()
    {
        this.gameObject.SetActive(false);
        KeyboardControl.TypingEnabled = true;
        keyboardController.ResumeTimer();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
