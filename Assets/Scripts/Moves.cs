using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private int maxMoves = 3;
    private int moves = 3;

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, name + ".txt");
        
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                moves = Int32.Parse(reader.ReadToEnd());
                SetText();
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveToString();
    }

    public bool IsMoveTime()
    {
        if (--moves == 0) 
        {
            moves = maxMoves;
            SetText();
            return true;
        }

        SetText();
        return false;
    }

    private void SaveToString()
    {
        using (StreamWriter file = new StreamWriter(Path.Combine(Application.persistentDataPath, name + ".txt")))
        {
            file.Write($"{moves}");
        }
    }

    private void SetText()
    {
        text.text = $"{moves}";
        text2.text = $"{moves}";
    }
}
