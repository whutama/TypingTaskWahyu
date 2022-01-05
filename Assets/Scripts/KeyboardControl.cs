using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardControl : MonoBehaviour
{
    public GameObject board;
    public GameObject altButton;
    public GameObject ctrlButton;
    public GameObject shiftButton;
    public GameObject backspaceButton;
    public GameObject enterButton;
    public GameObject sp1Button;
    public GameObject sp2Button;
    public GameObject sp3Button;
    public GameObject sp4Button;
    public GameObject sp5Button;
    private GameObject inputText;
    private TextMeshPro inp;
    private GameObject targetText;
    private TextMeshPro tar;
    private GameObject infoText;
    private TextMeshPro log;
    private PhraseController phraseController;
    private SaveData save;
    public int lastCorrect = 0;
    public static int totalKeyPress = 0;
    private int previousKeyPress = 0;
    public static int totalBackspace = 0;
    private int previousBackspace = 0;
    public static bool redText = false;
    public static bool start = false;
    public static float timer = 0.0f;
    private float previousTimer = 0.0f;

    public static int sentenceCounter = 0;
    public int sentenceTarget = 3;
    public static int sessionCounter = 0;
    public int sessionTarget = 10;
    private StartButtonScriptV2 startButton;
    private QuitButtonScript quitButton;
    public static bool TypingEnabled = true;
    public static string mistypes = "";
    public static List<string> typingLog = new List<string>();
    public static string typingLogTemp = "";
    public static List<string> typingTime = new List<string>();
    public static string typingTimeTemp = "";
    private List<string> typingCorrect = new List<string>();

    public void IncreaseLastCorrect()
    {
        lastCorrect++;
    }

    public bool IsDifferent()
    {
        //Debug.Log("inp: " + inp.text.Substring(lastCorrect, inp.text.Length - lastCorrect) + ";");
        //Debug.Log("tar: " + tar.text.Substring(lastCorrect, inp.text.Length - lastCorrect) + ";");
        if (inp.text.Substring(lastCorrect, inp.text.Length - lastCorrect) == tar.text.Substring(lastCorrect, inp.text.Length - lastCorrect))
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
            Debug.Log(tar.text.Substring(lastCorrect, inp.text.Length - lastCorrect));
            mistypes += tar.text.Substring(lastCorrect, inp.text.Length - lastCorrect);
            return false;
        }
    }

    public bool IsEqual()
    {
        if(inp.text == tar.text)
        {
            
            Debug.LogWarning("NEXT PHRASE!!");
            tar.text = phraseController.NextPhrase();
            typingCorrect.Add(tar.text);
            typingLog.Add(typingLogTemp);
            typingLogTemp = "";
            typingTime.Add(typingTimeTemp);
            typingTimeTemp = "";
            inp.text = "";
            lastCorrect = 0;
            sentenceCounter++;
            Debug.LogWarning(sentenceCounter);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PauseTimer()
    {
        Debug.LogWarning("PAUSE TIMER");
        start = false;
        save.WriteWord("Session " + sessionCounter + " -> totalKeyPress: " + (totalKeyPress - previousKeyPress) + " totalBackspace: " + (totalBackspace - previousBackspace) + " timer: " + (timer - previousTimer));
        save.WriteWord("Average WPM: " + ((totalKeyPress - previousKeyPress - totalBackspace + previousBackspace) / (timer - previousTimer) * 12));
        log.text = "WPM: " + ((totalKeyPress - previousKeyPress - totalBackspace + previousBackspace) / (timer - previousTimer) * 12) + "        " + sessionCounter.ToString();
        previousKeyPress = totalKeyPress;
        previousBackspace = totalBackspace;
        previousTimer = timer;
        TypingEnabled = false;
        board.GetComponent<Renderer>().material.color = Color.gray;
        startButton.Show();
        quitButton.Show();
    }

    public void ResumeTimer()
    {
        Debug.LogWarning("RESUME TIMER");
        if (start == false)
        {
            start = true;
            sentenceCounter = 0;
            board.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void EndTimer()
    {
        Debug.LogWarning("END TIMER");
        start = false;
        save.WriteWord("Session " + sessionCounter + " -> totalKeyPress: " + (totalKeyPress - previousKeyPress) + " totalBackspace: " + (totalBackspace - previousBackspace) + " timer: " + (timer - previousTimer));
        save.WriteWord("Average WPM: " + ((totalKeyPress - previousKeyPress - totalBackspace + previousBackspace) / (timer - previousTimer) * 12));
        log.text = "Average WPM: " + ((totalKeyPress - previousKeyPress - totalBackspace + previousBackspace) / (timer - previousTimer) * 12);
        save.WriteWord("Mistypes: " + mistypes);
        save.WriteWord("totalKeyPress: " + totalKeyPress + " totalBackspace: " + totalBackspace + " timer: " + timer + "\n\n");
        for(int i = 0; i < typingLog.Count; i++)
        {
            save.WriteWord(typingCorrect[i]);
            save.WriteWord(typingLog[i]);
            save.WriteWord(typingTime[i]);
        }
        save.WriteClose();
        TypingEnabled = false;
        board.GetComponent<Renderer>().material.color = Color.black;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputText = GameObject.FindGameObjectWithTag("Input");
        inp = inputText.GetComponent<TextMeshPro>();
        targetText = GameObject.FindGameObjectWithTag("Target");
        tar = targetText.GetComponent<TextMeshPro>();
        phraseController = this.gameObject.GetComponent<PhraseController>();
        tar.text = phraseController.NextPhrase();
        typingCorrect.Add(tar.text);
        save = this.gameObject.GetComponent<SaveData>();

        infoText = GameObject.Find("InfoText");
        log = infoText.GetComponent<TextMeshPro>();
        startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<StartButtonScriptV2>();
        quitButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<QuitButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && TypingEnabled == true)
        {
            backspaceButton.GetComponent<Renderer>().material.color = Color.red;
            //Debug.Log(lastCorrect);
            if (inp.text.Length > 0)
            {
                totalKeyPress++;
                typingLogTemp += "B";
                typingTimeTemp += timer.ToString("#.00") + ", ";
                totalBackspace++;
                inp.text = inp.text.Substring(0, inp.text.Length - 1);
                if (redText == true)
                {
                    //Debug.LogWarning(inp.text.Substring(lastCorrect, inp.text.Length - lastCorrect));
                    if (inp.text.Substring(lastCorrect, inp.text.Length - lastCorrect) == "<color=\"red\">")
                    {
                        redText = false;
                        inp.text = inp.text.Substring(0, inp.text.Length - 13);
                    }
                }
                else
                {
                    lastCorrect--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && TypingEnabled == true)
        {
            sp1Button.GetComponent<Renderer>().material.color = Color.red;
            sp2Button.GetComponent<Renderer>().material.color = Color.red;
            sp3Button.GetComponent<Renderer>().material.color = Color.red;
            sp4Button.GetComponent<Renderer>().material.color = Color.red;
            sp5Button.GetComponent<Renderer>().material.color = Color.red;
            totalKeyPress++;
            typingLogTemp += " ";
            typingTimeTemp += timer.ToString("#.00") + ", ";
            if (redText == false)
            {
                inp.text += " ";
                if (IsDifferent() == false)
                {
                    inp.text = inp.text.Substring(0, inp.text.Length - 1);
                    inp.text += "<color=\"red\">";
                    inp.text += "_";
                    redText = true;
                    
                }
                else
                {
                    IncreaseLastCorrect();
                }
            }
            else
            {
                inp.text += " ";
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterButton.GetComponent<Renderer>().material.color = Color.red;
            if (Application.isEditor)
            {
                print("We are running this from inside of the editor!");
                startButton.Resume();
                quitButton.Hide();
            }
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            backspaceButton.GetComponent<Renderer>().material.color = Color.white;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            sp1Button.GetComponent<Renderer>().material.color = Color.white;
            sp2Button.GetComponent<Renderer>().material.color = Color.white;
            sp3Button.GetComponent<Renderer>().material.color = Color.white;
            sp4Button.GetComponent<Renderer>().material.color = Color.white;
            sp5Button.GetComponent<Renderer>().material.color = Color.white;
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            enterButton.GetComponent<Renderer>().material.color = Color.white;
        }
        if (start == true)
        {
            timer += Time.deltaTime;
            //if (timer >= 180)
            //{
            //    EndTimer();
            //}
            
            if(sentenceCounter == sentenceTarget)
            {
                sessionCounter++;
                if (sessionCounter == sessionTarget)
                {
                    EndTimer();
                    sentenceCounter = 1000;
                }
                else
                {
                    PauseTimer();
                }
            }
        }
    }
}
