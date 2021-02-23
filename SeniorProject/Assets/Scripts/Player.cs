using UnityEngine; 
using System.Collections;

public delegate void DeadEventHandler();

public class Player : Character // using inheritence to give functionality from one to another
{
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

    public event DeadEventHandler Dead;

    private SpriteRenderer spriteRenderer;
    public Rigidbody2D MyRigidBody { get; set; }

    private Vector2 startPos;

    [SerializeField] public Stat healthStat;

    [SerializeField] private Transform[] groundPoints;

    [SerializeField] private float groundRadius;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private bool airControl;

    [SerializeField] private float jumpForce;
    
    [SerializeField] private float immortalTime;

    [SerializeField] GameObject DeathUI;

    private bool run;

    private bool immortal = false;
    public bool Jump { get; set; }
    public bool Run { get; set; }
    public bool OnGround { get; set; }
    public override bool isDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentVal <= 0;
        }
    }
    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    public override void Death()
    {
        Debug.Log("you died!!");
        DeathUI.gameObject.SetActive(true);

        PlayfabManager.SendLeaderboard(GameManager.Instance.collectedCandy); // or is it CollectedCandy?

        MyRigidBody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrentVal = healthStat.MaxVal;
        transform.position = startPos;
    }

    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidBody = GetComponent<Rigidbody2D>();
        healthStat.Initialize();
    }

    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
    }

    // fixed Update is called once per frame
    void FixedUpdate() // this function runs a fixed amount of times based on time step 
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleLayers();
        }
    }

    public bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentVal <= 0;
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (MyRigidBody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }

        if (!Attack && (OnGround || airControl))
        {
            MyRigidBody.velocity = new Vector2(horizontal * movementSpeed, MyRigidBody.velocity.y);
        }

        if (Jump && MyRigidBody.velocity.y == 0)
        {
            MyRigidBody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleRun()
    {
        if (run && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
        {
            MyAnimator.SetTrigger("run");
            MyRigidBody.velocity = Vector2.zero;
        }
    }

    private void HandleInput() // handleinput() for attacking and jumping etc 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // jump = true;
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            // Attack = true; 
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // run = true; 
            MyAnimator.SetTrigger("run");
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
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowKnife(value);
        }
    }

    private IEnumerator IndicateImmortal()
    {
        // this function makes the player flash while immortal
        while (immortal)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 10; // this is the amount of damage taken when struck 

            if (!isDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Candy")
        {
            GameManager.Instance.CollectedCandy++;
            Destroy(other.gameObject);
        }
    }
}
