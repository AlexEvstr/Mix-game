using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TicTacToeGameController : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI scoreText;
    public Sprite redPlaneSprite;
    public Sprite bluePlaneSprite;

    private SceneTransitionManager _sceneTransitionManager;
    [SerializeField] private ButtonsController _buttonsController;
    [SerializeField] private CanvasShake _canvasShake;
    [SerializeField] private GameObject _winEffect;

    private int[] board = new int[9];
    private bool playerTurn = true;
    private int playerScore;
    private int aiScore;
    private int _canEffects;

    void Start()
    {
        _canEffects = PlayerPrefs.GetInt("isEffectsEnabled", 1);
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
            SetButtonAlpha(buttons[i], 0);
        }
        LoadScores();
        ResetBoard();
        UpdateScoreText();
    }

    void OnButtonClick(int index)
    {
        if (board[index] == 0 && playerTurn)
        {
            board[index] = 1;
            buttons[index].GetComponent<Image>().sprite = redPlaneSprite;
            SetButtonAlpha(buttons[index], 1);
            if (CheckWin(1))
            {
                infoText.text = "PLAYER WINS!";
                playerScore++;
                _buttonsController.PlayWinSound();

                if (_canEffects == 1)
                {
                    _canvasShake.Shake();
                    GameObject win = Instantiate(_winEffect);
                    Destroy(win, 1);
                }
                
                SaveScores();
                UpdateScoreText();
                DisableButtons();
                StartCoroutine(RestartGameWithDelay());
                return;
            }
            if (IsBoardFull())
            {
                infoText.text = "IT'S A DRAW!";
                StartCoroutine(RestartGameWithDelay());
                return;
            }
            playerTurn = false;
            infoText.text = "";
            StartCoroutine(AITurnWithDelay());
        }
    }

    IEnumerator AITurnWithDelay()
    {
        yield return new WaitForSeconds(1);
        AITurn();
    }

    void AITurn()
    {
        int bestMove = FindBestMove();
        board[bestMove] = 2;
        buttons[bestMove].GetComponent<Image>().sprite = bluePlaneSprite;
        SetButtonAlpha(buttons[bestMove], 1);
        if (CheckWin(2))
        {
            infoText.text = "AI WINS!";
            aiScore++;
            _buttonsController.PlayLooseSound();

            if (_canEffects == 1)
            {
                _canvasShake.Shake();
            }

            SaveScores();
            UpdateScoreText();
            DisableButtons();
            StartCoroutine(RestartGameWithDelay());
            return;
        }
        if (IsBoardFull())
        {
            infoText.text = "IT'S A DRAW!";
            StartCoroutine(RestartGameWithDelay());
            return;
        }
        playerTurn = true;
        infoText.text = "YOUR TURN";
    }

    int FindBestMove()
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                board[i] = 2;
                if (CheckWin(2)) return i;
                board[i] = 0;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                board[i] = 1;
                if (CheckWin(1))
                {
                    board[i] = 0;
                    return i;
                }
                board[i] = 0;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                return i;
            }
        }
        return 0;
    }

    bool CheckWin(int player)
    {
        int[,] winPatterns = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };
        for (int i = 0; i < 8; i++)
        {
            if (board[winPatterns[i, 0]] == player &&
                board[winPatterns[i, 1]] == player &&
                board[winPatterns[i, 2]] == player)
            {
                return true;
            }
        }
        return false;
    }

    bool IsBoardFull()
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator RestartGameWithDelay()
    {
        yield return new WaitForSeconds(2);
        ResetBoard();
    }

    void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = 0;
            buttons[i].GetComponent<Image>().sprite = null;
            buttons[i].interactable = true;
            SetButtonAlpha(buttons[i], 0); // Reset alpha to 0
        }
        playerTurn = true;
        infoText.text = "YOUR TURN";
    }

    void DisableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    void SetButtonAlpha(Button button, float alpha)
    {
        Color color = button.GetComponent<Image>().color;
        color.a = alpha;
        button.GetComponent<Image>().color = color;
    }

    void UpdateScoreText()
    {
        scoreText.text = $"{playerScore}       VS       {aiScore}";
    }

    void SaveScores()
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("AIScore", aiScore);
        PlayerPrefs.Save();
    }

    void LoadScores()
    {
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        aiScore = PlayerPrefs.GetInt("AIScore", 0);
    }

    public void BackToHome()
    {
        _sceneTransitionManager.LoadScene("MenuScene");
    }
}
