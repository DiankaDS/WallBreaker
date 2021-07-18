using UnityEngine;

public enum BrickColor { Blue, Red, Yellow, Green }

public class Brick : MonoBehaviour
{
    [SerializeField] protected Material[] materials;
    [SerializeField] protected Material fadeMaterial;

    public bool isMarked = false;
    public static bool isGame = true;

    protected WallSection wall;
    private BrickColor color;

    public void InitBrick(WallSection wall) {
        this.wall = wall;
        int randomColor = Random.Range(0, 4);
        this.color = (BrickColor)randomColor;
        this.GetComponent<MeshRenderer>().materials = new Material[] { materials[randomColor] };
    }

    public BrickColor GetColor() {
        return color;
    }

    public virtual bool IsBrickMatch(BrickColor neededColor) {
        return color == neededColor;
    }

    public void OnMouseDown()
    {
        if (isGame)
        {
            isGame = false;
            wall.OnTouchBrick(this);
        }
    }

    public void DeleteWall() {
        wall.RemoveSection();
    }

    public void FadeColor()
    {
        this.GetComponent<MeshRenderer>().material = fadeMaterial;
    }

    public virtual void UnfadeColor()
    {
        this.GetComponent<MeshRenderer>().material = materials[(int)color];
    }
}
