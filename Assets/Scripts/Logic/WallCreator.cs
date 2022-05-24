using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WallCreator : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Statistics statistics;
    [SerializeField] private Moves moves;
    [SerializeField] private WallCreator wallOtherSide;
    [SerializeField] private AudioSource movingAudio;
    [SerializeField] private LastStep lastStep;
    private List<WallSection> wallSections = new List<WallSection>();
    private float wallSize = 1.15f;
    private string path;
    private int maxSections = 6;
    private int maxBricks = 8;
    
    private static int pointsPerTurn = 0;

    private string ConvertToString()
    {
        string[] result = new string[wallSections.Count];
        for (int i = 0; i < wallSections.Count; i++)
        {
            result[i] = wallSections[i].ConvertToString();
        }

        return String.Join("\n", result);
    }

    private void SaveWall()
    {
        string data = ConvertToString();

        using (StreamWriter file = new StreamWriter(path))
        {
            file.Write(data);
        }
    }

    private async Task OpenWall(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                int i = 0;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    CreateWallSection(wallSections.Count, line);
                    i++;
                }
            }
        }
    }

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, name + ".txt");

        if (File.Exists(path))
        {
            OpenWall(path);
        }
        else
        {
            InitWall(maxSections);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveWall();
        }
    }

    public void InitWall(int size)
    {
        for (int i = size - 1; i >= 0; i--) 
        {
            CreateWallSection(i);
        }
    }

    private void CreateWallSection(int delta, string data = null) 
    {
        WallSection cube = Instantiate(wallPrefab, transform).GetComponent<WallSection>();
        cube.transform.position += new Vector3(-delta * wallSize, 0, 0);

        if (data == null)
        {
            wallSections.Insert(0, cube);
            cube.InitWall(maxBricks);
        }
        else 
        {
            wallSections.Add(cube);
            cube.OpenSection(data);
        }
    }

    public void OnTouchSection(WallSection wall, int brickIndex, BrickColor color)
    {
        int index = wallSections.IndexOf(wall);
        OnTouchSection(index, brickIndex, color);
        wallOtherSide.OnTouchOtherWall(index, brickIndex, color);
    }

    public void OnTouchSection(int index, int brickIndex, BrickColor color)
    {
        if (index < wallSections.Count && index >= 0 && !wallSections[index].IsEmpty())
        {
            if (index + 1 < wallSections.Count && !wallSections[index + 1].IsEmpty())
            {
                wallSections[index + 1].OnTouchBrick(brickIndex, color);
            }

            if (index > 0 && !wallSections[index - 1].IsEmpty())
            {
                wallSections[index - 1].OnTouchBrick(brickIndex, color);
            }
        }
    }

    public void OnTouchOtherWall(int index, int brickIndex, BrickColor color)
    {
        if (index < wallSections.Count && index >= 0 && !wallSections[index].IsEmpty())
        {
            wallSections[index].OnTouchBrick(brickIndex, color);
        }
    }

    public void EndTouch()
    {
        DeleteMarkedCubes();
        wallOtherSide.DeleteMarkedCubes();
        Invoke("MoveWalls", 0.4f);
    }

    public void DeleteMarkedCubes()
    {
        int points = 0;

        foreach (WallSection wall in wallSections) 
        {
            points += wall.RemoveAndCountMarked();
        }

        CountPoints(points);
    }

    private void MoveWalls()
    {
        statistics.SetScores(pointsPerTurn);
        pointsPerTurn = 0;

        if (moves.IsMoveTime())
        {
            MoveWall();
            wallOtherSide.MoveWall();
        }

        CheckDangerous();
        Brick.isGame = true;
    }

    private void CheckDangerous()
    {
        if (GetFullSectionsCount() > 13 || wallOtherSide.GetFullSectionsCount() > 13)
        {
            lastStep.ShowDangerous();
        }
        else
        {
            lastStep.StopShowDangerous();
        }
    }

    public void DeleteLine(int index, bool isFirst = true)
    {
        int points = 0;

        foreach (WallSection wall in wallSections) 
        {
           if (wall.RemoveBrick(index)) 
           {
               points++;
           }
        }

        CountPoints(points);

        if (isFirst)
        {
            wallOtherSide.DeleteLine(index, false);
            Invoke("MoveWalls", 0.4f);
        }
    }

    private void MoveWall()
    {
        int i = 0;

        while (i < wallSections.Count)
        {
            if (wallSections[i].IsEmpty())
            {
                Destroy(wallSections[i].gameObject);
                wallSections.Remove(wallSections[i]);
                break;
            }
            else
            {
                wallSections[i].GetComponent<WallSection>().transform.position += new Vector3(-wallSize, 0, 0);
                i++;
            }
        }
        
        CreateWallSection(0);
        movingAudio.Play();
    }

    private void CountPoints(int value)
    {
        pointsPerTurn += value;
    }

    public void HideSections()
    {
       foreach (WallSection i in wallSections) 
       {
           i.HideCubes();
       }
    }

    public void ShowSections()
    {
       foreach (WallSection i in wallSections) 
       {
           i.ShowCubes();
       }
    }

    public int GetSectionsCount()
    {
        return wallSections.Count;
    }

    public int GetFullSectionsCount()
    {
        return wallSections.Where(x => !x.IsEmpty()).Count();
    }

    public void RemoveForContinue(int maxNumber)
    {
        for (int i = 0; i < 3; i++)
        {
            maxNumber--;
            if (wallSections.Count > maxNumber)
            {
                wallSections[maxNumber].RemoveAllBricks();
                Destroy(wallSections[maxNumber].gameObject);
                wallSections.Remove(wallSections[maxNumber]);
            }
        }
    }

    public void RemoveSection(WallSection wall)
    {
        Destroy(wall.gameObject);
        wallSections.Remove(wall);
    }
}
