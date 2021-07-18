using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void RestartGame()
    {
        click.Play();
        Brick.isGame = true;

        SideController.activeWall = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
