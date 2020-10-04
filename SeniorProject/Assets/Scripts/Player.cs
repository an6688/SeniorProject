using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidbody2D;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() // this function runs a fixed amount of times based on time step 
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        
        _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;

            transform.localScale = playerScale; 
        }
    }
}
