using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyInput : MonoBehaviour
{
    public string keyCode;
    private GameObject inputText;
    private TextMeshPro inp;
    private KeyboardControl keyboardController;
    private SaveData save;

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.FindGameObjectWithTag("Input");
        inp = inputText.GetComponent<TextMeshPro>();
        keyboardController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyboardControl>();
        save = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode) && KeyboardControl.TypingEnabled == true)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            KeyboardControl.totalKeyPress++;
            KeyboardControl.typingLogTemp += keyCode;
            KeyboardControl.typingTimeTemp += KeyboardControl.timer.ToString("#.00") + ", ";
            if (KeyboardControl.redText == false)
            {
                inp.text += keyCode;
                if (keyboardController.IsDifferent() == false)
                {
                    inp.text = inp.text.Substring(0,inp.text.Length - 1);
                    inp.text += "<color=\"red\">";
                    inp.text += keyCode;
                    KeyboardControl.redText = true;
                    Debug.Log(keyCode + "_");
                    KeyboardControl.mistypes += keyCode + "_";
                }
                else
                {
                    if(keyboardController.lastCorrect == 0)
                    {
                        save.WriteWord("First word time: " + KeyboardControl.timer);
                    }
                    keyboardController.IncreaseLastCorrect();
                }
            }
            else
            {
                inp.text += keyCode;
            }
            if (keyboardController.IsEqual())
            {
                save.WriteWord("totalKeyPress: " + KeyboardControl.totalKeyPress + " totalBackspace: " + KeyboardControl.totalBackspace + " timer: " + KeyboardControl.timer);
            }
        }
        if (Input.GetKeyUp(keyCode))
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
