using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    [SerializeField] private RawImage secondImage;
    [SerializeField] private List<Texture> images = new List<Texture>();
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
        if (moves == images.Count)
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
        else
        {
            moves = images.Count;
        }
        PlayerPrefs.SetInt("moves", moves);
        SetImages();
    }

    private void SetImages()
    {
        GetComponent<RawImage>().texture = images[moves - 1];
        secondImage.texture = images[moves - 1];
    }
}
