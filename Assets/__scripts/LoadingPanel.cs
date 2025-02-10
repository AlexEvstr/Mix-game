using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(LoadManuScene());
    }

    private IEnumerator LoadManuScene()
    {
        yield return new WaitForSeconds(2.75f);
        SceneManager.LoadScene("MenuScene");
    }
}