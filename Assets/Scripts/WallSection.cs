using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSection : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject rocketPrefab;

    private List<Brick> bricks = new List<Brick>();
    private static bool isMouseEnabled = true;
    private float brickSize = 1.15f;

    public static float rocketChance = 2000f;
    
    public string ToString()
    {
        string[] result = new string[bricks.Count];
        for (int i = 0; i < bricks.Count; i++)
        {
            result[i] = bricks[i].ToString();
        }
        return String.Join("|", result);
    }

    public void OpenSection(string data)
    {
        if (data != "")
        {
            string[] colors = data.Split('|');
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == "10")
                {
                    CreateRocket();
                }
                else
                {
                    CreateBrick(colors[i]);
                }
            }
        }
    }

    public bool IsEmpty()
    {
        return bricks.Count == 0;
    }

    public void InitWall(int size)
    {
        for (int i = 0; i < size; i++) 
        {
            if (UnityEngine.Random.Range(0, 100000) > rocketChance) 
            {
                CreateBrick();
            }
            else
            {
                CreateRocket();
            }
        }
    }

    private void CreateBrick(string color = null)
    {
        Brick cube = Instantiate(brickPrefab, transform).GetComponent<Brick>();
        cube.transform.position += new Vector3(0, bricks.Count * brickSize, 0);
        cube.InitBrick(this, color);
        bricks.Add(cube);
    }

    private void CreateRocket()
    {
        Rocket cube = Instantiate(rocketPrefab, transform).GetComponent<Rocket>();
        cube.transform.position += new Vector3(0, bricks.Count * brickSize, 0);
        cube.InitBrick(this);
        bricks.Add(cube);
    }

    public void OnTouchRocket(Rocket cube)
    {
        int brickIndex = bricks.IndexOf(cube);
        transform.parent.GetComponent<WallCreator>().DeleteLine(brickIndex);
    }

    public void OnTouchBrick(Brick cube)
    {
        if (!cube.isMarked)
        {
            BrickColor color = cube.GetColor();
            int brickIndex = bricks.IndexOf(cube);
            OnTouchBrick(brickIndex, color);
            Invoke("DeleteMarked", 0.2f);
        }
    }

    public void OnTouchBrick(int brickIndex, BrickColor color)
    {
        if (brickIndex < bricks.Count && 
            brickIndex >= 0 && 
            bricks[brickIndex].IsBrickMatch(color) && 
            !bricks[brickIndex].isMarked)
        {
            bricks[brickIndex].isMarked = true;
            OnTouchBrick(brickIndex + 1, color);
            OnTouchBrick(brickIndex - 1, color);
            transform.parent.GetComponent<WallCreator>().OnTouchSection(this, brickIndex, color);
        }
    }

    public void DeleteMarked()
    {
        transform.parent.GetComponent<WallCreator>().EndTouch();
    }

    public int RemoveAndCountMarked()
    {
        int points = 0;
        for (int i = 0; i < bricks.Count; i++)
        {
            Brick cube = bricks[i];
            if (cube.isMarked)
            {
                bricks.Remove(cube);
                cube.Destroy();
                points++;
                i--;
            }
        }
        return points;
    }
    
    public bool RemoveBrick(int index)
    {
        if (index < bricks.Count)
        {
            Brick cube = bricks[index];
            bricks.Remove(cube);
            cube.Destroy();
            return true;
        }
        
        return false;
    }

    public void HideCubes()
    {
       foreach (Brick i in bricks) 
       {
           i.FadeColor();
       }
    }

    public void ShowCubes()
    {
       foreach (Brick i in bricks) 
       {
           i.UnfadeColor();
       }
    }

    public void RemoveSection()
    {
        transform.parent.GetComponent<WallCreator>().RemoveSection(this);
    }

    public void RemoveAllBricks()
    {
        while (bricks.Count > 0)
        {
            RemoveBrick(0);
        }
    }
}
