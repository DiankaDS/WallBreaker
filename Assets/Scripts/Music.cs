using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Music : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource click;
    [SerializeField] private Button button2;

    public void OnPointerDown (PointerEventData data)
    {
        click.Play();

        if (music.mute)
        {
            ChangeImages(gameObject, true, false);
            ChangeImages(button2.gameObject, true, false);

        }
        else
        {
            ChangeImages(gameObject, false, true);
            ChangeImages(button2.gameObject, false, true);
        }

        music.mute = !music.mute;
    }

    private void ChangeImages(GameObject obj, bool value0, bool value1)
    {
        obj.transform.GetChild(0).gameObject.SetActive(value0);
        obj.transform.GetChild(1).gameObject.SetActive(value1);
    }
}
