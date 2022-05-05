using System;
using UnityEngine;
using UnityEngine.UI;

public class BestResult : MonoBehaviour
{
    [SerializeField] private Statistics statistics;
    private int bestScores = 0;
    private int bestLevel = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("best_level"))
        {
            bestLevel = PlayerPrefs.GetInt("best_level");
        }

        if (PlayerPrefs.HasKey("best_scores"))
        {
            bestScores = PlayerPrefs.GetInt("best_scores");
        }

        SetText();
    }

    private void SetText()
    {
        GetComponent<Text>().text = $"Score: {bestScores}    Level: {bestLevel}";
    }

    private void SetBetter()
    {
        if (statistics.GetScores() > bestScores)
        {
            bestScores = statistics.GetScores();
            bestLevel = statistics.GetLevel();
        }

        PlayerPrefs.SetInt("best_scores", bestScores);
        PlayerPrefs.SetInt("best_level", bestLevel);
    }

    private void OnEnable()
    {
        SetBetter();
        SetText();
    }

    private void OnDestroy()
    {
        SetBetter();
        PlayerPrefs.Save();
    }
}
