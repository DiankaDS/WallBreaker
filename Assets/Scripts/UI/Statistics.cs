using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text textLeft;
    [SerializeField] private Text textRight;
    private int scores;
    private int level;
    private int delta;

    private void Start()
    {
        scores = 0;
        level = 1;
        delta = 100;

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
            WallSection.rocketChance *= 0.9f;
            PlayerPrefs.SetFloat("rocket_chance", WallSection.rocketChance);
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
