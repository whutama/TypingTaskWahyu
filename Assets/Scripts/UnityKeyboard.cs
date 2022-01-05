using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnityKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    private KeyboardControl keyboardController;
    private GameObject inputTextObject;
    private TextMeshPro inp;
    private SaveData save;

    public void TurnOnKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        overlayKeyboard.text = "a";
    }

    // Start is called before the first frame update
    void Start()
    {
        keyboardController = GameObject.FindGameObjectWithTag("GameController").GetComponent<KeyboardControl>();
        inputTextObject = GameObject.FindGameObjectWithTag("Input");
        inp = inputTextObject.GetComponent<TextMeshPro>();
        save = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveData>();
//        overlayKeyboard.text = "a";
    }

    // Update is called once per frame
    void Update()
    {
        if (overlayKeyboard != null && overlayKeyboard.text != "a")
        {
            if (overlayKeyboard.text.Length == 0)
            {
                KeyboardControl.totalKeyPress++;
                KeyboardControl.totalBackspace++;
                inp.text = inp.text.Substring(0, inp.text.Length - 1);
                if (KeyboardControl.redText == true)
                {
                    //Debug.LogWarning(inp.text.Substring(lastCorrect, inp.text.Length - lastCorrect));
                    if (inp.text.Substring(keyboardController.lastCorrect, inp.text.Length - keyboardController.lastCorrect) == "<color=\"red\">")
                    {
                        KeyboardControl.redText = false;
                        inp.text = inp.text.Substring(0, inp.text.Length - 13);
                    }
                }
                else
                {
                    keyboardController.lastCorrect--;
                }
                overlayKeyboard.text = "a";
            }
            else
            {
                inputText = overlayKeyboard.text.Substring(1, overlayKeyboard.text.Length - 1);
                KeyboardControl.totalKeyPress++;
                if (KeyboardControl.redText == false)
                {
                    inp.text += inputText;
                    if (keyboardController.IsDifferent() == false)
                    {
                        inp.text = inp.text.Substring(0, inp.text.Length - 1);
                        inp.text += "<color=\"red\">";
                        inp.text += inputText;
                        KeyboardControl.redText = true;
                    }
                    else
                    {
                        keyboardController.IncreaseLastCorrect();
                    }
                }
                else
                {
                    inp.text += inputText;
                }
                overlayKeyboard.text = "a";
                if (keyboardController.IsEqual())
                {
                    save.WriteWord("Raycast - totalKeyPress: " + KeyboardControl.totalKeyPress + " totalBackspace: " + KeyboardControl.totalBackspace + " timer: " + KeyboardControl.timer);
                }
            }
        }
    }
}
