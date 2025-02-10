using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _menuWindow;
    private SceneTransitionManager _sceneTransitionManager;

    private void OnEnable()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void OpenSettings()
    {
        _settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsWindow.SetActive(false);
    }

    public void OpenGame_1()
    {
        _sceneTransitionManager.LoadScene("Game_1");
    }

    public void OpenGame_2()
    {
        _sceneTransitionManager.LoadScene("Game_2");
    }

    public void OpenGame_3()
    {
        _sceneTransitionManager.LoadScene("Game_3");
    }

    public void OpenGame_4()
    {
        _sceneTransitionManager.LoadScene("Game_4");
    }
}