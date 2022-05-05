using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Music : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject[] buttonsOn;
    [SerializeField] private GameObject[] buttonsOff;

    private void Start()
    {
        if (PlayerPrefs.HasKey("music_mute") && PlayerPrefs.GetInt("music_mute") == 1)
        {
            Mute(true);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        click.Play();
        Mute(!music.mute);
    }

    private void Mute(bool isMute)
    {
        music.mute = isMute;
        ChangeImage(isMute);
        PlayerPrefs.SetInt("music_mute", isMute ? 1 : 0);
    }

    private void ChangeImage(bool isMute)
    {
        foreach (GameObject button in buttonsOn)
        {
            button.SetActive(!isMute);
        }

        foreach (GameObject button in buttonsOff)
        {
            button.SetActive(isMute);
        }
    }
}
