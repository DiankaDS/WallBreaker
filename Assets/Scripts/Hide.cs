using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SideController wall;
    [SerializeField] private AudioSource click;

    public void OnPointerDown (PointerEventData data)
    {
        PlayAudio();
        wall.HideCubes();
    }

    public void OnPointerUp (PointerEventData data)
    {
        PlayAudio();
        wall.ShowCubes();
    }

    private void PlayAudio() 
    {
        click.Play();
    }
}
