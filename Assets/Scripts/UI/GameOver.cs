using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private SideController side;
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource fall;
    [SerializeField] private LastStep lastStep;

    public static bool isGameOver = false;
    private bool isFirstFail = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey("isGameOver") && PlayerPrefs.GetInt("isGameOver") == 1)
        {
            ShowGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        fall.Play();
        other.GetComponent<Brick>().DeleteWall();
        PlayerPrefs.SetInt("isGameOver", 1);
        ShowGameOver();
    }

    private void ShowGameOver()
    {
        Brick.isGame = false;
        isGameOver = true;

        gameOverCanvas.gameObject.SetActive(true);
        
        if (!isFirstFail)
        {
            buttonContinue.gameObject.SetActive(false);
        }
        isFirstFail = false;
    }

    public void SetContinue()
    {
        click.Play();
        Invoke("DeleteSectionAndContinue", 0.1f);
    }

    public void DeleteSectionAndContinue()
    {
        isGameOver = false;
        PlayerPrefs.SetInt("isGameOver", 0);
        lastStep.StopShowDangerous();
        side.ContinueGame();
        HideMenu();
    }

    public void HideMenu()
    {
        Brick.isGame = true;
        gameOverCanvas.gameObject.SetActive(false);
    }
}
