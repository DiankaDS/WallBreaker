using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideController : MonoBehaviour
{
    [SerializeField] private List<WallCreator> walls = new List<WallCreator>();
    [SerializeField] private Canvas[] ui;

    public static int activeWall = 0;
    public Animator animator;

    public void HideCubes()
    {
        walls[activeWall].HideSections();
    }

    public void ShowCubes()
    {
        walls[activeWall].ShowSections();
    }

    public void ContinueGame() {
        foreach (WallCreator wall in walls)
        {
            wall.RemoveForContinue();
        }
    }

    public void RotateCamera()
    {
        Brick.isGame = false;

        ui[activeWall].gameObject.SetActive(false);

        if (activeWall == 0) {
            activeWall = 1;
            animator.SetBool("move", true);
        }
        else 
        {
            activeWall = 0;
            animator.SetBool("move", false);
        }

        Invoke("SetActiveUI", 3);
    }

    private void SetActiveUI()
    {
        ui[activeWall].gameObject.SetActive(true);
        Brick.isGame = true;
    }
}
