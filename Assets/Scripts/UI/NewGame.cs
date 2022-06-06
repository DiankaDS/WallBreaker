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
        WallSection.rocketChance = 0.02f;
        PlayerPrefs.SetInt("isGameOver", 0);
        PlayerPrefs.SetInt("moves", 0);
        PlayerPrefs.SetFloat("rocket_chance", 0.02f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
