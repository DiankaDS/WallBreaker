using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] protected Material[] materials;
    [SerializeField] protected Material fadeMaterial;
    [SerializeField] private GameObject particlesPrefab;

    public bool isMarked = false;
    public static bool isGame = true;

    protected WallSection wall;
    private BrickColor color;

    public virtual string ConvertToString()
    {
        return $"{(int)color}";
    }

    public void InitBrick(WallSection wall, string colorToSet = null) 
    {
        int colorKey;
        this.wall = wall;
        
        if (colorToSet == null)
        {
            colorKey = UnityEngine.Random.Range(0, Enum.GetNames(typeof(BrickColor)).Length);
        }
        else 
        {
            colorKey = Int32.Parse(colorToSet);
        }

        color = (BrickColor)colorKey;
        GetComponent<MeshRenderer>().materials = new Material[] { materials[colorKey] };
    }

    public BrickColor GetColor() 
    {
        return color;
    }

    public virtual bool IsBrickMatch(BrickColor neededColor) 
    {
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

    public void DeleteWall() 
    {
        wall.RemoveSection();
    }

    public void FadeColor()
    {
        GetComponent<MeshRenderer>().material = fadeMaterial;
    }

    public virtual void UnfadeColor()
    {
        GetComponent<MeshRenderer>().material = materials[(int)color];
    }

    public void Destroy()
    {
        CreateParticles();
        Destroy(gameObject);
    }
    
    private void CreateParticles()
    {
        ParticleSystem particles = Instantiate(particlesPrefab, transform).GetComponent<ParticleSystem>();
        particles.GetComponent<ParticleSystemRenderer>().material = materials[(int)color];
        particles.transform.parent = null;
        particles.transform.LookAt(Camera.main.transform);
        particles.Play();
    }
}
