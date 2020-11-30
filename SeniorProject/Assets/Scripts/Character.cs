using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // Character is a super class that works for both the player and enemy

    public Animator MyAnimator { get; private set; }

    [SerializeField] protected float movementSpeed;

    protected bool facingRight;

    public bool Attack { get; set; }

    // knifeposition is a place holder variable for now, enemies may throw objects
    [SerializeField] private Transform knifePosition;

    // placeholder for enemies throwing object
    [SerializeField] private GameObject knifePrefab;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("CharStart: ");
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        // transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;

        transform.localScale = playerScale;
    }

    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject) Instantiate(knifePrefab, knifePosition.position, Quaternion.identity);
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePosition.position, Quaternion.identity);
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }
}
