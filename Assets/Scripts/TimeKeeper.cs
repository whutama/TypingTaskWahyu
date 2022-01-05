using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeKeeper : MonoBehaviour
{
    public GameObject startButton;
    public GameObject infoText;
    private TextMeshPro log;
    public float timeStart, timeEnd, timeBetween;
    // Start is called before the first frame update
    void Start()
    {
        log = infoText.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        log.text = timeStart.ToString();
    }

    public void StartButtonPressed()
    {
        //remove button
        startButton.GetComponent<Renderer>().enabled = false;
        //start the timer
        timeStart = Time.time;
        //activate keyboard
        //show text

    }

    public void NextPhrase()
    {
        //timer

        //
    }

    public void SessionEnd()
    {
        //stop timer
        timeEnd = Time.time;
        timeBetween = timeEnd - timeStart;
        ShowRestScene();
    }

    public void ShowRestScene()
    {
        ;
    }

    public void TimerDebug()
    {
        log.text = "a";
    }
}
