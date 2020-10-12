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

    private bool attack;

    private bool walk;

    private bool run; 

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        HandleInput();
    }

    // fixed Update is called once per frame
    void FixedUpdate() // this function runs a fixed amount of times based on time step 
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (walk && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            myAnimator.SetBool("walk", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            myAnimator.SetBool("walk", false);
        }

        if (run && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            myAnimator.SetBool("run", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            myAnimator.SetBool("run", false);
        }
    }

    private void HandleAttacks() // for jump attack later, thats why it is attackS
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            _myRigidbody2D.velocity = Vector2.zero; 
        }
    }

    private void HandleInput() // handleinput() for attacking and jumping etc 
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            attack = true; 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true; 
        }

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

    private void ResetValues()
    {
        // resets player condition back to idle
        attack = false;
        walk = false; 
    }
}
