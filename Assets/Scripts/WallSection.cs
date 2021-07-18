using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSection : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject rocketPrefab;

    private List<Brick> bricks = new List<Brick>();
    private static bool isMouseEnabled = true;
    private float brickSize = 1.15f;

    public static float rocketChance = 2000f;
    
    void Start()
    {
        this.InitWall(8);
    }

    public bool IsEmpty()
    {
        return bricks.Count == 0;
    }

    public void InitWall(int size)
    {
        for(int i = 0; i < size; i++) 
        {
            if (Random.Range(0, 100000) > rocketChance) 
            {
                CreateBrick();
            }
            else
            {
                CreateRocket();
            }
            
        }
    }

    private void CreateBrick()
    {
        Brick cube = Instantiate(brickPrefab, this.transform).GetComponent<Brick>();
        cube.transform.position += new Vector3(0, bricks.Count * brickSize, 0);
        cube.InitBrick(this);
        bricks.Add(cube);
    }

    private void CreateRocket()
    {
        Rocket cube = Instantiate(rocketPrefab, this.transform).GetComponent<Rocket>();
        cube.transform.position += new Vector3(0, bricks.Count * brickSize, 0);
        cube.InitBrick(this);
        bricks.Add(cube);
    }

    public void OnTouchRocket(Rocket cube)
    {
        int brickIndex = bricks.IndexOf(cube);
        this.transform.parent.GetComponent<WallCreator>().DeleteLine(brickIndex);
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
        if (
            brickIndex < bricks.Count && 
            brickIndex >= 0 && 
            bricks[brickIndex].IsBrickMatch(color) && 
            !bricks[brickIndex].isMarked
        )
        {
            bricks[brickIndex].isMarked = true;
            this.OnTouchBrick(brickIndex + 1, color);
            this.OnTouchBrick(brickIndex - 1, color);
            this.transform.parent.GetComponent<WallCreator>().OnTouchSection(this, brickIndex, color);
        }
    }

    public void DeleteMarked()
    {
        this.transform.parent.GetComponent<WallCreator>().EndTouch();
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
                Destroy(cube.gameObject);
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
            Destroy(cube.gameObject);
            return true;
        }
        
        return false;
    }

    public void HideCubes()
    {
       foreach (Brick i in bricks) {
           i.FadeColor();
       }
    }

    public void ShowCubes()
    {
       foreach (Brick i in bricks) {
           i.UnfadeColor();
       }
    }

    public void RemoveSection() {
        this.transform.parent.GetComponent<WallCreator>().RemoveSection(this);
    }

    public void RemoveAllBricks() {
        while (bricks.Count > 0)
        {
            RemoveBrick(0);
        }
    }
}
