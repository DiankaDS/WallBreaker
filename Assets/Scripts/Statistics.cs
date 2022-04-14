using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private int scores = 0;
    private int level = 1;
    private int delta = 100;

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, name + ".txt");
        
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string[] data = reader.ReadToEnd().Split('|');
                scores = Int32.Parse(data[0]);
                level = Int32.Parse(data[1]);
                SetText();
            }
        }
    }

    private void SaveToString()
    {
        using (StreamWriter file = new StreamWriter(Path.Combine(Application.persistentDataPath, name + ".txt")))
        {
            file.Write($"{scores}|{level}");
        }
    }

    private void OnApplicationQuit()
    {
        SaveToString();
    }

    public void SetScores(int points)
    {
        scores += level * points;

        if (scores > delta) 
        {
            level++;
            delta += level * level * 100;
            WallSection.rocketChance *= 0.98f;
        }

        SetText();
    }

    private void SetText()
    {
        text.text = $"Score: {scores}    Level: {level}";
        text2.text = $"Score: {scores}    Level: {level}";
    }
}
