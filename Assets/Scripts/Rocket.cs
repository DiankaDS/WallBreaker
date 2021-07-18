using UnityEngine;

public class Rocket : Brick
{
    public void InitBrick(WallSection wall) {
        this.wall = wall;
    }

    public override bool IsBrickMatch(BrickColor neededColor) {
        return false;
    }

    public void OnMouseDown()
    {
        if (isGame)
        {
            isGame = false;
            wall.OnTouchRocket(this);
        }
    }

    public override void UnfadeColor()
    {
        this.GetComponent<MeshRenderer>().material = materials[0];
    }
}
