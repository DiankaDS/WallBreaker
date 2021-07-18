using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Help : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource click;
    [SerializeField] private Canvas text;

    public void OnPointerDown (PointerEventData data)
    {
        PlayAudio();
        text.gameObject.SetActive(true);
    }

    public void OnPointerUp (PointerEventData data)
    {
        PlayAudio();
        text.gameObject.SetActive(false);
    }

    private void PlayAudio() 
    {
        click.Play();
    }
}
