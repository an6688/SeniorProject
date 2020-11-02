using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character is a super class that works for both the player and enemy

    private Animator myAnimator;

    [SerializeField] private Transform knifePosition; // knifeposition is a place holder variable for now, enemies may throw objects

    [SerializeField] private GameObject knifePrefab; // placeholder for enemies throwing object

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
