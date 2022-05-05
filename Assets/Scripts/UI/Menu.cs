using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource click;
    [SerializeField] private Canvas menu;

    public void OnPointerDown(PointerEventData data)
    {
        PlayAudio();
        if (!GameOver.isGameOver)
        {
            if (Brick.isGame)
            {
                Brick.isGame = false;
                menu.gameObject.SetActive(true);
            }
            else
            {
                Brick.isGame = true;
                menu.gameObject.SetActive(false);
            }
        }
    }

    private void PlayAudio()
    {
        click.Play();
    }
}
