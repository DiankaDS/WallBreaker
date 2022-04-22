using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonCntinue;
    [SerializeField] private SideController side;
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource fall;

    private bool isFirstFail = true;

    private void OnTriggerEnter(Collider other)
    {
        fall.Play();
        other.GetComponent<Brick>().DeleteWall();

        Brick.isGame = false;
        gameOverText.gameObject.SetActive(true);
        buttonRestart.gameObject.SetActive(true);
        
        if (isFirstFail)
        {
            buttonCntinue.gameObject.SetActive(true);
            isFirstFail = false;
        }
    }

    public void SetContinue()
    {
        click.Play();

        if (isFirstFail)
        {
            Invoke("DeleteSectionAndContinue", 0.1f);
        }
    }

    public void DeleteSectionAndContinue()
    {
        side.ContinueGame();
        HideMenu();
    }

    public void HideMenu()
    {
        Brick.isGame = true;
        gameOverText.gameObject.SetActive(false);
        buttonRestart.gameObject.SetActive(false);
        buttonCntinue.gameObject.SetActive(false);
    }
}
