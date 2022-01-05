using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    private StreamWriter writer;
    private string path;
    public TextAsset phr;

    public void WriteWord(string word)
    {
        Debug.Log("SaveData.cs wrote: " + word);
        writer.WriteLine(word);
    }

    public void WriteClose()
    {
        writer.Close();
    }

    public void CopyPhrases()
    {
        WriteClose();
        path = Application.persistentDataPath + "/phrases2.txt";
        writer = new StreamWriter(path);
        var textArray = phr.text.Split('\n');
        for(int i = 0; i < 500; i++)
        {
            textArray[i] = textArray[i].TrimEnd('\r', '\n');
            writer.WriteLine(textArray[i]);
        }
        writer.Close();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("SaveData.cs awoke");
        path = Application.persistentDataPath + string.Format("/text-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);
        writer = new StreamWriter(path, true);
        //CopyPhrases();
    }
    private void OnApplicationQuit()
    {
        if (writer.BaseStream != null)
        {
            this.gameObject.GetComponent<KeyboardControl>().EndTimer();
        }
    }
}
