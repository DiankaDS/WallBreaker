using UnityEngine;

public class Rocket : Brick
{
    public void InitBrick(WallSection wall) 
    {
        this.wall = wall;
    }

    public override bool IsBrickMatch(BrickColor neededColor) 
    {
        return false;
    }

    public void OnMouseDown()
    {
        if (isGame && !GameOver.isGameOver)
        {
            isGame = false;
            wall.OnTouchRocket(this);
        }
    }

    public override string ConvertToString()
    {
        return "10";
    }

    public override void UnfadeColor()
    {
        GetComponent<MeshRenderer>().material = materials[0];
    }
}
