using UnityEngine;

public class GameConstructions : MonoBehaviour
{
    [SerializeField] private GameObject[] _constructions;

    private void OnEnable()
    {
        Time.timeScale = 1;
        int constructionIndex = Random.Range(0, _constructions.Length);
        _constructions[constructionIndex].SetActive(true);
    }
}