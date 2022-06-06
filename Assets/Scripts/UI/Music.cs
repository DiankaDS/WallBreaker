using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject imgMusicOn;
    [SerializeField] private GameObject imgMusicOff;
    [SerializeField] private GameObject imgSoundsOn;
    [SerializeField] private GameObject imgSoundsOff;

    private bool isSounds;

    private void Start()
    {
        isSounds = true;
        if (PlayerPrefs.HasKey("music_mute") && PlayerPrefs.GetInt("music_mute") == 1)
        {
            Mute(true);
        }

        if (PlayerPrefs.HasKey("sounds_mute") && PlayerPrefs.GetInt("sounds_mute") == 0)
        {
            MuteAll();
        }
    }

    public void OnClickMusic()
    {
        click.Play();
        Mute(!music.mute);
    }

    public void OnClickSounds()
    {
        click.Play();
        MuteAll();
    }

    private void Mute(bool isMute)
    {
        music.mute = isMute;
        ChangeMusicImage(isMute);
        PlayerPrefs.SetInt("music_mute", isMute ? 1 : 0);
    }

    private void MuteAll()
    {
        ChangeSoundsImage(isSounds);
        isSounds = !isSounds;
        GetComponent<AudioListener>().enabled = isSounds;
        PlayerPrefs.SetInt("sounds_mute", isSounds ? 1 : 0);
    }

    private void ChangeMusicImage(bool isMute)
    {
        imgMusicOn.SetActive(!isMute);
        imgMusicOff.SetActive(isMute);
    }

    private void ChangeSoundsImage(bool isMute)
    {
        imgSoundsOn.SetActive(!isMute);
        imgSoundsOff.SetActive(isMute);
    }
}
