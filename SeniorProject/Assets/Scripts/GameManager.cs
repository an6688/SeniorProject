using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // this class should really be called CandyManager... 
    private static GameManager instance;
    
    [SerializeField] public GameObject candyPrefab;

    [SerializeField] private TMP_Text candyText;

    [SerializeField] public int collectedCandy;

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

    public GameObject CandyPrefabs => candyPrefab;

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

    // Instantiate the Prefab somewhere between -10.0 and 10.0 on the x-z plane
    void Start()
    {
    }


    
}
