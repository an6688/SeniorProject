using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidbody2D;

    private Animator myAnimator; // in Character.cs

    [SerializeField]
    private float movementSpeed; // in Character.cs 

    private bool facingRight; // in Character.cs

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool attack;

    private bool run; 

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.GetComponent<Rigidbody>().useGravity = false;

    }


    void Update()
    {
        HandleInput();
        
    }

    // fixed Update is called once per frame
    void FixedUpdate() // this function runs a fixed amount of times based on time step 
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleInput();

        HandleAttacks();

        HandleLayers();

        HandleRun();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (_myRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            _myRigidbody2D.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }

        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0
        }

        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
        {
            _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks() // for jump attack later, thats why it is attackS
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            _myRigidbody2D.velocity = Vector2.zero;
        }
    }

    private void HandleRun()
    {
        if (run && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
        {
            myAnimator.SetTrigger("run");
            _myRigidbody2D.velocity = Vector2.zero;
        }
    }

    private void HandleInput() // handleinput() for attacking and jumping etc 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            attack = true; 
        }

        if (Input.GetKey(KeyCode.LeftShift))
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
        run = false;
        jump = false; 
    }

    private bool IsGrounded()
    {
        if (_myRigidbody2D.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true; 
                    }
                }
            }
        }
        return false; 
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1,1);
        }
        else
        {
            {
                myAnimator.SetLayerWeight(1, 0);
            }
        }
    }
}
