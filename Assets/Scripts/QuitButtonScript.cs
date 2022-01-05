using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonScript : MonoBehaviour
{
    private KeyboardControl keyboardController;
    private GameObject button;

    private void Start()
    {
        keyboardController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyboardControl>();
        button = this.transform.GetChild(2).gameObject;
        //hide keyboard at start
    }
    public void Hide()
    {
        button.SetActive(false);
    }
    public void Quit()
    {
        keyboardController.EndTimer();
    }
    public void Show()
    {
        button.SetActive(true);
    }
}
