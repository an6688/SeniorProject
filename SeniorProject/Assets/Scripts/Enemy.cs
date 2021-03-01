using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField] private float meleeRange;

    [SerializeField] private float throwRange;

    [SerializeField] private Transform knifePosition;

    [SerializeField] private GameObject knifePrefab;

    private Vector2 startPos;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private bool dropItem = true;


    public override void Start()
    {
        base.Start();
        //Makes the RemoveTarget function listen to the player's Dead event
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null & !isDead) currentState.Execute();

        LookAtTarget();
    }

    public virtual void ThrowKnife(int value)
    {
        if (facingRight) //If we are facing right then throw the knife to the right
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePosition.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else //If we are facing to the lft then throw the knife to the left.
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePosition.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }


    public override IEnumerator TakeDamage()
    {
        health -= 5;
        if (!isDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            if (dropItem)
            {
                GameObject candy = (GameObject)Instantiate(GameManager.Instance.CandyPrefabs,
                    new Vector3(transform.position.x, transform.position.y + 1),
                    Quaternion.identity);
                dropItem = false; 
            }
            MyAnimator.SetTrigger("die");
            yield return null; 
        }
    }

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(
                    transform.position, 
                    Target.transform.position) 
                       <= meleeRange;
            }

            return false; 
        }
    }

    /// Indicates if the enemy is in throw range
    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(
                    transform.position, 
                    Target.transform.position) 
                       <= throwRange;

            }
            return false;
        }
    }

    private void RemoveTarget()
    {
        //Removes the target
        Target = null;

        //Changes the state to a patrol state
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        // currentState?.Exit(); 

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            MyAnimator.SetFloat("speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
        else if (currentState is PatrolState)
        {
            ChangeDirection();
        }
        else if (currentState is RangedState)
        {
            Target = null;
            ChangeState(new IdleState());
        }

        //    if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) ||
        // (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override bool isDead
    {
        get
        {
            return health <= 0; 
        }
    }

    public override void Death()
    {
        // the commented */*/ lines of code allow the enemy to be respawned
        // but for now the enemy object is destroyed 
        // meaning the enemy dies and does not respawn
        /*MyAnimator.ResetTrigger("die");
        MyAnimator.SetTrigger("idle");
        health = 30;
        transform.position = startPos; */
        Destroy(gameObject,-1);
    }
}
