using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SideController wall;
    [SerializeField] private AudioSource click;

    public void OnPointerDown (PointerEventData data)
    {
        click.Play();
        Invoke("Rotate", 0.1f);
    }

    public void Rotate()
    {
        if (Brick.isGame) {
            wall.RotateCamera();
        }
    }
}