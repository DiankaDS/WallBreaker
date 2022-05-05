using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private SideController side;
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource fall;

    public static bool isGameOver = false;
    private bool isFirstFail = true;

    private void OnTriggerEnter(Collider other)
    {
        fall.Play();
        other.GetComponent<Brick>().DeleteWall();

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

        if (isFirstFail)
        {
            Invoke("DeleteSectionAndContinue", 0.1f);
        }
    }

    public void DeleteSectionAndContinue()
    {
        isGameOver = false;
        side.ContinueGame();
        HideMenu();
    }

    public void HideMenu()
    {
        Brick.isGame = true;
        gameOverCanvas.gameObject.SetActive(false);
    }
}
