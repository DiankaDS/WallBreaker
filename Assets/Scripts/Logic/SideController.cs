using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideController : MonoBehaviour
{
    [SerializeField] private List<WallCreator> walls = new List<WallCreator>();
    [SerializeField] private Canvas[] ui;
    [SerializeField] private Slider speedSlider;
    private int[] speeds;
    public static int speed;
    public static int activeWall = 0;
    public Animator animator;

    void Awake()
    {
        speeds = new int[]{1, 2, 4, 40};

        if (PlayerPrefs.HasKey("speed_camera"))
        {
            SetSpeed(PlayerPrefs.GetInt("speed_camera"));
        }
        else
        {
            SetSpeed(0);
        }
    }

    public void OnSliderChanged()
    {
        int value = (int)speedSlider.value;
        speed = speeds[value];
        PlayerPrefs.SetInt("speed_camera", value);
    }

    public void HideCubes()
    {
        walls[activeWall].HideSections();
    }

    public void ShowCubes()
    {
        walls[activeWall].ShowSections();
    }

    public void ContinueGame() 
    {
        int maxNumber = walls.Select(x => x.GetSectionsCount()).Max();
        
        foreach (WallCreator wall in walls)
        {
            wall.RemoveForContinue(maxNumber);
        }
    }

    public void RotateCamera()
    {
        animator.speed = speed;
        Brick.isGame = false;

        ui[activeWall].gameObject.SetActive(false);

        if (activeWall == 0) 
        {
            activeWall = 1;
            animator.SetBool("move", true);
        }
        else 
        {
            activeWall = 0;
            animator.SetBool("move", false);
        }

        Invoke("SetActiveUI", 3f / speed);
    }

    private void SetSpeed(int value)
    {
        speed = speeds[value];
        speedSlider.value = value;
    }

    private void SetActiveUI()
    {
        ui[activeWall].gameObject.SetActive(true);
        Brick.isGame = true;
    }
}
