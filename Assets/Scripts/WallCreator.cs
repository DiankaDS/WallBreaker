using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCreator : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Statistics statistics;
    [SerializeField] private Moves moves;
    [SerializeField] private WallCreator wallOtherSide;
    private List<WallSection> wallSections = new List<WallSection>();
    private float wallSize = 1.15f;
    
    private static int pointsPerTurn = 0;

    private void Start()
    {
        this.InitWall(6);
    }

    public void InitWall(int size)
    {
        for(int i = size - 1; i >= 0; i--) 
        {
            CreateWallSection(i);
        }
    }

    private void CreateWallSection(int delta) 
    {
        WallSection cube = Instantiate(wallPrefab, this.transform).GetComponent<WallSection>();
        cube.transform.position += new Vector3(-delta * wallSize, 0, 0);
        wallSections.Insert(0, cube);
    }

    public void OnTouchSection(WallSection wall, int brickIndex, BrickColor color)
    {
        int index = wallSections.IndexOf(wall);
        this.OnTouchSection(index, brickIndex, color);
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

        foreach (WallSection wall in wallSections) {
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

        Brick.isGame = true;
    }

    public void DeleteLine(int index, bool isFirst = true)
    {
        int points = 0;

        foreach (WallSection wall in wallSections) {
           if (wall.RemoveBrick(index)) {
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
    }

    private void CountPoints(int value)
    {
        pointsPerTurn += value;
    }

    public void HideSections()
    {
       foreach (WallSection i in wallSections) {
           i.HideCubes();
       }
    }

    public void ShowSections()
    {
       foreach (WallSection i in wallSections) {
           i.ShowCubes();
       }
    }

    public void RemoveForContinue()
    {
        int index = 5;
        for (int i = 0; i < 3; i++)
        {
            wallSections[index++].RemoveAllBricks();
        }
    }

    public void RemoveSection(WallSection wall)
    {
        Destroy(wall.gameObject);
        wallSections.Remove(wall);
    }
}
