using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _mainSprites;
    [SerializeField] private Sprite[] _sprites;

    public string indexArrayKey = "SavedIndexArray"; // Ключ для сохранения массива индексов в PlayerPrefs
    private int[] indexArray; // Массив индексов
    private int index;

    private void Start()
    {
        
        if (PlayerPrefs.GetString("SameColor", "no") == "yes")
        {
            LoadIndexes();
            for (int i = 0; i < _mainSprites.Length; i++)
            {
                _mainSprites[i].sprite = _sprites[indexArray[i]];
            }
        }
        else
        {
            indexArray = new int[_mainSprites.Length];
            for (int i = 0; i < _mainSprites.Length; i++)
            {
                
                index = Random.Range(0, _sprites.Length);
                indexArray[i] = index;
                _mainSprites[i].sprite = _sprites[index];
                
            }
        }

        SaveIndexes();


    }

    void SaveIndexes()
    {        
        PlayerPrefs.SetInt(indexArrayKey + "_Length", indexArray.Length);

        for (int i = 0; i < indexArray.Length; i++)
        {
            PlayerPrefs.SetInt(indexArrayKey + "_" + i, indexArray[i]);
        }
        PlayerPrefs.Save();
    }

    void LoadIndexes()
    {
        int arrayLength = PlayerPrefs.GetInt(indexArrayKey + "_Length", 0);

        indexArray = new int[arrayLength];

        for (int i = 0; i < arrayLength; i++)
        {
            indexArray[i] = PlayerPrefs.GetInt(indexArrayKey + "_" + i, 0);
        }
    }
}