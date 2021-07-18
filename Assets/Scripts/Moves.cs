using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private int maxMoves = 3;
    private int moves = 3;

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

    private void SetText()
    {
        text.text = $"{moves}";
        text2.text = $"{moves}";
    }
}
