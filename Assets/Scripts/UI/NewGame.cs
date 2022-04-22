using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void RestartGame()
    {
        click.Play();

        File.Delete(Path.Combine(Application.persistentDataPath, "WallCreator1.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "WallCreator2.txt"));
        Brick.isGame = true;
        SideController.activeWall = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
