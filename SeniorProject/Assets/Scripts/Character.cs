using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // Character is a super class that works for both the player and enemy

    protected Animator myAnimator; // protected means it can be accessed by the class itself and anything inheriting from it 

    [SerializeField] private Transform knifePosition; // knifeposition is a place holder variable for now, enemies may throw objects

    [SerializeField] private GameObject knifePrefab; // placeholder for enemies throwing object

    [SerializeField] protected float movementSpeed;

    protected bool facingRight;

    protected bool Attack { get; set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("CharStart: ");
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        // transform.localScale = new Vector3(transform.localScale.x * 1, 1, 1);
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;

        transform.localScale = playerScale;
    }
}
