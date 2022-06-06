using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text textLeft;
    [SerializeField] private Text textRight;
    private int scores = 0;
    private int level = 1;
    private int delta = 100;

    private void Start()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }

        if (PlayerPrefs.HasKey("scores"))
        {
            scores = PlayerPrefs.GetInt("scores");
        }

        SetText();
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

        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("scores", scores);
        SetText();
    }

    public void ResetScores()
    {
        scores = 0;
        level = 1;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("scores", scores);
        SetText();
    }

    public int GetScores()
    {
        return scores;
    }

    public int GetLevel()
    {
        return level;
    }

    private void SetText()
    {
        textLeft.text = $"Score: {scores}    Level: {level}";
        textRight.text = $"Score: {scores}    Level: {level}";
    }
}
