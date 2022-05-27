using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private AudioSource fall;

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
        ShowGameOver();
        other.GetComponent<Brick>().DeleteWall();
    }

    private void ShowGameOver()
    {
        Brick.isGame = false;
        isGameOver = true;
        PlayerPrefs.SetInt("isGameOver", 1);

        gameOverCanvas.gameObject.SetActive(true);
        
        if (!isFirstFail)
        {
            buttonContinue.gameObject.SetActive(false);
        }
        isFirstFail = false;
    }
}
