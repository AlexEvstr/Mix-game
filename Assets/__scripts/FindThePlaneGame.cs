using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FindThePlaneGame : MonoBehaviour
{
    public Image targetPlaneImage;
    public List<Sprite> planeSprites;
    public List<Image> planeImages;

    private Sprite targetPlaneSprite;
    private SceneTransitionManager _sceneTransitionManager;
    [SerializeField] private ButtonsController _buttonsController;
    [SerializeField] private CanvasShake _canvasShake;
    [SerializeField] private GameObject _win;
    private int _canEffect;

    void Start()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _canEffect = PlayerPrefs.GetInt("isEffectsEnabled", 1);
        SetupGame();
    }

    void SetupGame()
    {
        targetPlaneSprite = planeSprites[Random.Range(0, planeSprites.Count)];
        targetPlaneImage.sprite = targetPlaneSprite;

        foreach (var image in planeImages)
        {
            Sprite randomSprite;
            do
            {
                randomSprite = planeSprites[Random.Range(0, planeSprites.Count)];
            } while (randomSprite == targetPlaneSprite);

            image.sprite = randomSprite;
            image.GetComponent<Button>().onClick.RemoveAllListeners();
            image.GetComponent<PlaneDragAndDrop>().ResetPosition();
            image.gameObject.SetActive(true);
        }

        int randomIndex = Random.Range(0, planeImages.Count);
        planeImages[randomIndex].sprite = targetPlaneSprite;
        planeImages[randomIndex].GetComponent<Button>().onClick.AddListener(() => OnTargetPlaneClicked(planeImages[randomIndex]));
    }

    void OnTargetPlaneClicked(Image image)
    {
        _buttonsController.PlayGoodShotSound();
        _buttonsController.PlayWinSound();

        if (_canEffect == 1)
        {
            _canvasShake.Shake();
            GameObject win = Instantiate(_win);
            Destroy(win, 1);
        }
        StartCoroutine(EnlargeAndDisappear(image));
    }

    IEnumerator EnlargeAndDisappear(Image image)
    {
        Vector3 originalScale = image.rectTransform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            image.rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        image.rectTransform.localScale = targetScale;
        yield return new WaitForSeconds(0.5f);

        image.gameObject.SetActive(false);
        SetupGame();
    }

    public void BackToHome()
    {
        _sceneTransitionManager.LoadScene("MenuScene");
    }
}
