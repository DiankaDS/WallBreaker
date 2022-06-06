using UnityEngine;
using UnityEngine.EventSystems;

public class Help : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource click;
    [SerializeField] private Canvas text;

    public void OnPointerDown(PointerEventData data)
    {
        PlayAudio();
        if (Brick.isGame)
        {
            text.gameObject.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        PlayAudio();
        if (Brick.isGame)
        {
            text.gameObject.SetActive(false);
        }
    }

    private void PlayAudio() 
    {
        click.Play();
    }
}
