using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    private float transitionDuration = 0.25f;

    private void Awake()
    {
        if (transitionImage != null)
        {
            transitionImage.color = new Color(0, 0, 0, 0);
            transitionImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Transition image is not assigned.");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(HandleSceneTransition(sceneName));
        }
        else
        {
            Debug.LogError("Scene name is invalid.");
        }
    }

    private IEnumerator HandleSceneTransition(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack());

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeToBlack()
    {
        if (transitionImage != null)
        {
            float elapsedTime = 0;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float alphaValue = Mathf.Clamp01(elapsedTime / transitionDuration);
                transitionImage.color = new Color(0, 0, 0, alphaValue);
                yield return null;
            }
            transitionImage.color = new Color(0, 0, 0, 1);
        }
    }

    private IEnumerator FadeFromBlack()
    {
        if (transitionImage != null)
        {
            float elapsedTime = 0;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float alphaValue = Mathf.Clamp01(1 - (elapsedTime / transitionDuration));
                transitionImage.color = new Color(0, 0, 0, alphaValue);
                yield return null;
            }
            transitionImage.color = new Color(0, 0, 0, 0);
        }
    }
}
