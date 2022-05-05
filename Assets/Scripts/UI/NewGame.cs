using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private Statistics statistics;

    public void RestartGame()
    {
        click.Play();

        File.Delete(Path.Combine(Application.persistentDataPath, "WallCreator1.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "WallCreator2.txt"));
        statistics.ResetScores();
        Brick.isGame = true;
        GameOver.isGameOver = false;
        SideController.activeWall = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
