using System;
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
        if (PlayerPrefs.HasKey("moves"))
        {
            SetMoves(PlayerPrefs.GetInt("moves"));
        }
    }

    public bool IsMoveTime()
    {
        SetMoves(moves - 1);
        if (moves == maxMoves)
        {
            return true;
        }

        return false;
    }

    private void SetMoves(int value)
    {
        if (value > 0)
        {
            moves = value;
        }
        else {
            moves = maxMoves;
        }
        PlayerPrefs.SetInt("moves", moves);
        SetText();
    }

    private void SetText()
    {
        text.text = $"{moves}";
        text2.text = $"{moves}";
    }
}
