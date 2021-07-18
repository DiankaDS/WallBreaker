using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text text;
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

        text.text = "GAME OVER";
        Brick.isGame = false;
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

        text.text = "";
        Brick.isGame = true;
        buttonRestart.gameObject.SetActive(false);
        buttonCntinue.gameObject.SetActive(false);
    }
}
