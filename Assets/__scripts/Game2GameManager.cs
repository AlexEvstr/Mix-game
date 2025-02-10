using UnityEngine;
using System.Collections.Generic;

public class Game2GameManager : MonoBehaviour
{
    private SceneTransitionManager _sceneTransitionManager;
    public static Game2GameManager Instance;
    [SerializeField] private ButtonsController _buttonsController;
    [SerializeField] private CanvasShake _canvasShake;
    [SerializeField] private GameObject _win;
    private int _canEffect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _canEffect = PlayerPrefs.GetInt("isEffectsEnabled", 1);
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        ShufflePlanes();
    }

    public bool AllPlanesPlacedCorrectly()
    {
        Game2Plane[] planes = FindObjectsOfType<Game2Plane>();
        foreach (var plane in planes)
        {
            RectTransform planeRect = plane.GetComponent<RectTransform>();
            Game2Shadow shadow = GetShadowById(plane.ID);
            if (planeRect.anchoredPosition != shadow.GetComponent<RectTransform>().anchoredPosition)
                return false;
        }
        _buttonsController.PlayWinSound();
        if (_canEffect == 1)
        {
            GameObject win = Instantiate(_win);
            Destroy(win, 1);
            _canvasShake.Shake();
        }
        
        return true;
    }

    private Game2Shadow GetShadowById(int id)
    {
        Game2Shadow[] shadows = FindObjectsOfType<Game2Shadow>();
        foreach (var shadow in shadows)
        {
            if (shadow.ID == id)
                return shadow;
        }
        return null;
    }

    public void RestartGame()
    {
        ResetPlanesToInitialPositions();
        ShufflePlanes();
    }

    private void ShufflePlanes()
    {
        Game2Plane[] planes = FindObjectsOfType<Game2Plane>();
        List<Vector2> originalPositions = new List<Vector2>();

        foreach (var plane in planes)
        {
            originalPositions.Add(plane.GetComponent<RectTransform>().anchoredPosition);
        }

        Shuffle(originalPositions);

        for (int i = 0; i < planes.Length; i++)
        {
            planes[i].GetComponent<Game2DragAndDrop>().ResetPosition(originalPositions[i]);
        }
    }

    private void ResetPlanesToInitialPositions()
    {
        Game2Plane[] planes = FindObjectsOfType<Game2Plane>();
        foreach (var plane in planes)
        {
            plane.GetComponent<Game2DragAndDrop>().ResetPosition(plane.GetComponent<Game2DragAndDrop>().originalPosition);
        }
    }

    private void Shuffle(List<Vector2> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void BackToHome()
    {
        _sceneTransitionManager.LoadScene("MenuScene");
    }
}
