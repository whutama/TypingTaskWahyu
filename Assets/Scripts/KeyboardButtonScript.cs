using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButtonScript : MonoBehaviour
{
    public bool keyboard3DEnabled = true;
    private GameObject canvas;
    private GameObject[] keys;

    private void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("KeyboardButton");
        canvas = GameObject.Find("KeyboardCanvas");
        //hide keyboard at start
        HideShowKeyboard();
    }
    public void HideShowKeyboard()
    {
        if (keyboard3DEnabled == true)
        {
            keyboard3DEnabled = false;
            foreach (GameObject child in keys)
            {
                child.GetComponent<Renderer>().enabled = false;
            }
            canvas.SetActive(false);
        }
        else if(keyboard3DEnabled == false)
        {
            keyboard3DEnabled = true;
            foreach (GameObject child in keys)
            {
                child.GetComponent<Renderer>().enabled = true;
            }
            canvas.SetActive(true);
        }
    }
}
