using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject candyPrefab;

    [SerializeField] private TMP_Text candyText;

    private int collectedCandy;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public GameObject CandyPrefab
    {
        get
        {
            return candyPrefab;
        }
    }

    public int CollectedCandy
    {
        get
        {
            return collectedCandy;
        }

        set
        {
            candyText.text = value.ToString();
            this.collectedCandy = value;
        }
    }
}
