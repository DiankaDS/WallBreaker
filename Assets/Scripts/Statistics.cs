using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text text2;
    private int scores = 0;
    private int level = 1;
    private int delta = 100;

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
