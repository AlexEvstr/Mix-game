using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Game2DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector2 originalPosition;
    private Canvas canvas;

    [SerializeField] private ButtonsController _buttonsController;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        _buttonsController.PlayExplosionSound();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject shadow = eventData.pointerEnter;
        if (shadow != null && shadow.CompareTag("Shadow") && shadow.GetComponent<Game2Shadow>().ID == this.GetComponent<Game2Plane>().ID)
        {
            rectTransform.anchoredPosition = shadow.GetComponent<RectTransform>().anchoredPosition;
            _buttonsController.PlayGoodShotSound();
            CheckGameCompletion();
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    private void CheckGameCompletion()
    {
        if (Game2GameManager.Instance.AllPlanesPlacedCorrectly())
        {
            StartCoroutine(RestartGameWithDelay());
        }
    }

    private IEnumerator RestartGameWithDelay()
    {
        yield return new WaitForSeconds(2);
        Game2GameManager.Instance.RestartGame();
    }

    public void ResetPosition(Vector2 newPosition)
    {
        rectTransform.anchoredPosition = newPosition;
        originalPosition = newPosition;
    }

    public void SetOriginalPosition(Vector2 position)
    {
        originalPosition = position;
    }
}
