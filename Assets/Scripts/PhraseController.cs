using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PhraseController : MonoBehaviour
{
    public List<string> phrases;
    private int num = 0;
    //string�^���X�g�������_���ɕ��ёւ�
    public List<string> RandomSort(string[] array)
    {
        return array.OrderBy(a => Guid.NewGuid()).ToList();
    }

    //TextAsset��String�z��ɕϊ�
    public string[] ChangeStringArray(TextAsset textAsset)
    {
        return textAsset.text.ToLower().Replace("\r\n", "\n").Split(new[] { '\n', '\r' });
    }

    public List<string> LoadPhrases()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "phrases2.txt");

        var androidPath = Application.persistentDataPath + "/phrases2.txt";

        //StartCoroutine(PathManage(path));

        string[] phrases = File.ReadAllLines(androidPath);

        //return RandomSort(resultString.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).ToArray()).Take(5).ToList();
        //Take���̐������t���[�Y���擾
        //return RandomSort(phrases).Take(20).ToList();
        return RandomSort(phrases).ToList();
    }
    public string NextPhrase()
    {
        return phrases[num++].ToLower();
    }

    private void Awake()
    {
        phrases = LoadPhrases();
        //Debug.Log(phrases[0]);
    }
}
