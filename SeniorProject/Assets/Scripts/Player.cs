using UnityEngine;
using System.Collections;


    public class Player : Character // using inheritence to give functionality from one to another
    {
        // private Rigidbody2D _myRigidbody2D;

        /*private Animator myAnimator; // in Character.cs

    [SerializeField]f
    private float movementSpeed; // in Character.cs 

    private bool facingRight; // in Character.cs*/

        private static Player instance;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<Player>(); // from video 14.2. this is a singleton pattern. this works with only 1 player in a game. you cant use a singleton pattern if theres more than 1 player in a game, singleton works on one object at a time. 
                }

                return instance; 
            }
        }

        [SerializeField] private Transform[] groundPoints;

        [SerializeField] private float groundRadius;

        [SerializeField] private LayerMask whatIsGround;

        [SerializeField] private bool airControl;

        [SerializeField] private float jumpForce;

        // private bool attack; // in character.cs

        // private bool isGrounded;

        // private bool jump;

        private bool run;
        
        public Rigidbody2D MyRigidBody { get; set; }

        public bool Jump { get; set; }
        public bool Run { get; set; }

        public bool OnGround { get; set; }

        private Vector2 startPos;


        // Start is called before the first frame update
    public override void Start()
        {
            //facingRight = true; // in character.cs
            // Debug.Log("PlayerStart: ");
            base.Start();

            startPos = transform.position;
            MyRigidBody = GetComponent<Rigidbody2D>();
            // myAnimator = GetComponent<Animator>(); // in character.cs
            //myAnimator.GetComponent<Rigidbody>().useGravity = false;
        }


        void Update()
        {
            if (transform.position.y <= -14f)
            {
                MyRigidBody.velocity = Vector2.zero;
                transform.position = startPos;
            }

            HandleInput();
        
        }

        // fixed Update is called once per frame
        void FixedUpdate() // this function runs a fixed amount of times based on time step 
        {
            float horizontal = Input.GetAxis("Horizontal");

            // isGrounded = IsGrounded();
            OnGround = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleInput();

            // HandleAttacks();

            HandleLayers();

            HandleRun();

            // ResetValues();
        }

        private void HandleMovement(float horizontal)
        {
            /*if (_myRigidbody2D.velocity.y < 0)
            {
                MyAnimator.SetBool("land", true);
            }
            if (isGrounded && jump)
            {
                isGrounded = false;
                _myRigidbody2D.AddForce(new Vector2(0, jumpForce));
                MyAnimator.SetTrigger("jump");
            }

            if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
            {
                _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0
            }

            if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
            {
                _myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, _myRigidbody2D.velocity.y); // vector with an x value of -1 and a y value of 0
            }

            MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));*/

            if (MyRigidBody.velocity.y < 0)
            {
                myAnimator.SetBool("land", true);
            }

            if (!Attack && (OnGround || airControl))
            {
                MyRigidBody.velocity = new Vector2(horizontal * movementSpeed, MyRigidBody.velocity.y);
            }

            if (Jump && MyRigidBody.velocity.y == 0)
            {
                MyRigidBody.AddForce(new Vector2(0, jumpForce));
            }
            myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        //private void HandleAttacks() // for jump attack later, thats why it is attackS
        //{
        //    if (Attack && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        //    {
        //        MyAnimator.SetTrigger("attack");
        //        _myRigidbody2D.velocity = Vector2.zero;
        //    }
        //}

        private void HandleRun()
        {
            if (run && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
            {
                myAnimator.SetTrigger("run");
                MyRigidBody.velocity = Vector2.zero;
            }
        }

        private void HandleInput() // handleinput() for attacking and jumping etc 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // jump = true;
                myAnimator.SetTrigger("jump");
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                // Attack = true; 
                myAnimator.SetTrigger("attack");
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                // run = true; 
                myAnimator.SetTrigger("run");
            }
        }

        private void Flip(float horizontal)
        {
            if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
            {
                /*facingRight = !facingRight;

            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;

            transform.localScale = playerScale; */
                ChangeDirection(); 
            }
        }

        //private void ResetValues()
        //{
        //    // resets player condition back to idle
        //    Attack = false;
        //    run = false;
        //    jump = false; 
        //}

        private bool IsGrounded()
        {
            if (MyRigidBody.velocity.y <= 0)
            {
                foreach (Transform point in groundPoints)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].gameObject != gameObject)
                        {
                            // MyAnimator.ResetTrigger("jump");
                            // MyAnimator.SetBool("land", false);
                            return true; 
                        }
                    }
                }
            }
            return false; 
        }

        private void HandleLayers()
        {
            if (!OnGround)
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
