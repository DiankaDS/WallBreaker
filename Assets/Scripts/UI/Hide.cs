using UnityEngine;
using UnityEngine.EventSystems;

public class Hide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SideController wall;
    [SerializeField] private AudioSource click;

    public void OnPointerDown(PointerEventData data)
    {
        PlayAudio();
        if (Brick.isGame)
        {
            wall.HideCubes();
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        PlayAudio();
        if (Brick.isGame)
        {
            wall.ShowCubes();
        }
    }

    private void PlayAudio() 
    {
        click.Play();
    }
}
