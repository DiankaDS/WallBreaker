using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LastStep : MonoBehaviour
{
    [SerializeField] private Canvas dangerousCanvas;
    private bool isDangerous = false;
    private float duration = 2f;

    private IEnumerator Dangerous()
    {
        while (isDangerous)
        {
            dangerousCanvas.GetComponent<RawImage>().CrossFadeAlpha(0f, duration, false);
            yield return new WaitForSeconds (duration);
            dangerousCanvas.GetComponent<RawImage>().CrossFadeAlpha(1f, duration, false);
            yield return new WaitForSeconds (duration);
        }
    }

    public void ShowDangerous()
    {
        if (!isDangerous)
        {
            isDangerous = true;
            dangerousCanvas.gameObject.SetActive(true);
            StartCoroutine(Dangerous());
        }
    }

    public void StopShowDangerous()
    {
        if (isDangerous)
        {
            isDangerous = false;
            dangerousCanvas.gameObject.SetActive(false);
        }
    }
}
